using Microsoft.Extensions.Logging;
using MMS.EventBus.Abstractions;
using NotificationProcessor.API.Infrastructure;
using NotificationProcessor.API.IntegrationEvents.Events;
using NotificationProcessor.API.Model;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents
{

    public class NotificationRequestIntegrationEventHandler : IIntegrationEventHandler<EntityRegisteredIntegrationEvent>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<NotificationRequestIntegrationEventHandler> _logger;
        private readonly INotificationIntegrationEventService _notificationIntegrationEventService;
        public NotificationRequestIntegrationEventHandler(NotificationContext notificationContext,ILogger<NotificationRequestIntegrationEventHandler> logger,
             INotificationIntegrationEventService notificationIntegrationEventService)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _notificationIntegrationEventService = notificationIntegrationEventService;
        }
        public async Task Handle(EntityRegisteredIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                NotificationTransactionLog notificationLog = new NotificationTransactionLog()
                {
                    TrackingID = Guid.NewGuid(),
                    EntityID = @event.EntityID,
                    ComChannel = "test@gmail.com",
                    Recipient = "Command.API",
                    TopicID = @event.TopicID,
                    //NotificationDate = DateTime.Now,
                    NotificationStage = "Pending",
                    MessageBody="test"
                };
                await _notificationContext.NotificationTransactionLogs.AddAsync(notificationLog);
                await _notificationContext.SaveChangesAsync();
                await _notificationIntegrationEventService.SaveEventAndNotificationLogChangesAsync(@event);

            }
        }
    }
}
