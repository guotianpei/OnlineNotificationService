using ONP.Domain.Models;
using ONP.Domain.Seedwork;
using ONP.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONP.Infrastructure
{
    public class NotificationProcessorContext: DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        private NotificationProcessorContext(DbContextOptions<NotificationProcessorContext> options) : base(options)
        {
        }
        public NotificationProcessorContext(DbContextOptions<NotificationProcessorContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("NotificationContext::ctor ->" + this.GetHashCode());
        }
        public const string DEFAULT_SCHEMA = "ONP";
        public DbSet<NotificationRequest> NotificationRequests { get; set; }
        public DbSet<EntityProfile> EntityProfiles { get; set; }
        public DbSet<NotificationTransactionLog> NotificationTransactionLogs { get; set; }
        public DbSet<FailureNotifyCodes> FailureNotifyCodes { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        //public DbSet<IntegrationEventLogEntry> IntegrationEventLogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotificationRequestConfiguration());
            builder.ApplyConfiguration(new EntityProfileConfiguration());
            builder.ApplyConfiguration(new NotificationTransactionLogConfiguration());
            builder.ApplyConfiguration(new FailureNotifyCodesConfiguration());
            builder.ApplyConfiguration(new NotificationTemplateConfiguration());
            //builder.ApplyConfiguration(new IntegrationEventLogConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
    public class NotificationContextDesignFactory : IDesignTimeDbContextFactory<NotificationProcessorContext>
    {
        public NotificationProcessorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationProcessorContext>()
                .UseSqlServer("Server=DC01VI2WSSDV01.WV.CORE.HIM\\OPMDEV;Initial Catalog=OnlineNotification;Integrated Security = False; Persist Security Info = False; User ID = sa; Password = Pass@word");
            return new NotificationProcessorContext(optionsBuilder.Options, new NoMediator());
            //return null;
        }

        class NoMediator : IMediator
        {
            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            Task IMediator.Publish(object notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }

            Task IMediator.Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }

            Task<TResponse> IMediator.Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }


        }
    }
}
