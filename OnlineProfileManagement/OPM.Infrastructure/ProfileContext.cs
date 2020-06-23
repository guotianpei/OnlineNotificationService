using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using OPM.Domain.SeekWork;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Infrastructure.EntityConfigurations;

using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace OPM.Infrastructure
{
    public class ProfileContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "OPM";
        //public DbSet<DistributionGroup> Groups { get; set; }
        //public DbSet<ProfileDistributionGroup> ProfileGroups { get; set; }
        public DbSet<EntityProfile> EntityProfile { get; set; }
        public DbSet<ProfileComChannel> ProfileComChannels { get; set; }
     

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        //public ProfileContext(DbContextOptions<ProfileContext> options, IMediator mediator) : base(options)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        //    System.Diagnostics.Debug.WriteLine("ProfileContext::ctor ->" + this.GetHashCode());
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            //modelBuilder.ApplyConfiguration(new EntityProfileConfiguration());
            //modelBuilder.ApplyConfiguration(new ProfileEntityTypeConfiguration());
            // Other entities' configuration ...
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

    public class ProfileContextDesignFactory : IDesignTimeDbContextFactory<ProfileContext>
    {
        public ProfileContext CreateDbContext(string[] args)
        {
            // var optionsBuilder = new DbContextOptionsBuilder<ProfileContext>()
            //     .UseSqlServer("Server=localhost,5433;Database=OPM;User Id=SA;Password=Pass@word;");

            //// return new ProfileContext(optionsBuilder.Options, new NoMediator());
            // return new ProfileContext(optionsBuilder.Options);
            return null;
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

