using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MMS.EventBus.Abstractions;
using MMS.EventBus.Events;
using MMS.IntegrationEventLogEF.Services;
using MMS.IntegrationEventLogEF.Utilities;
using ONP.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents
{
    public class NotificationIntegrationEventService : INotificationIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly NotificationProcessorContext _notificationContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<NotificationIntegrationEventService> _logger;

        public NotificationIntegrationEventService(ILogger<NotificationIntegrationEventService> logger,
            IEventBus eventBus,
             NotificationProcessorContext notificationContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _notificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(notificationContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task SaveEventAndNotificationLogChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_notificationContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _notificationContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _notificationContext.Database.CurrentTransaction);
            });
        }
    }
}
