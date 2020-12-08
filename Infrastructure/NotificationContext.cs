using Domain;
using Infrastructure.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class NotificationContext: DbContext
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        private NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
        }
        public NotificationContext(DbContextOptions<NotificationContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            System.Diagnostics.Debug.WriteLine("NotificationContext::ctor ->" + this.GetHashCode());
        }
        public DbSet<NotificationRequest> NotificationRequests { get; set; }
        public DbSet<EntityProfile> EntityProfiles { get; set; }
        public DbSet<NotificationTransactionLog> NotificationTransactionLogs { get; set; }
        public DbSet<FailureNotifyCodes> FailureNotifyCodes { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<IntegrationEventLogEntry> IntegrationEventLogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NotificationRequestConfiguration());
            builder.ApplyConfiguration(new EntityProfileConfiguration());
            builder.ApplyConfiguration(new NotificationTransactionLogConfiguration());
            builder.ApplyConfiguration(new FailureNotifyCodesConfiguration());
            builder.ApplyConfiguration(new NotificationTemplateConfiguration());
            builder.ApplyConfiguration(new IntegrationEventLogConfiguration());
        }
    }
    public class NotificationContextDesignFactory : IDesignTimeDbContextFactory<NotificationContext>
    {
        public NotificationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationContext>()
                .UseSqlServer("Server=DC01VI2WSSDV01.WV.CORE.HIM\\OPMDEV;Initial Catalog=OnlineNotification;Integrated Security = False; Persist Security Info = False; User ID = sa; Password = Pass@word");
            return new NotificationContext(optionsBuilder.Options, new NoMediator());
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
