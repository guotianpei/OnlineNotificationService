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

    public class NotificationRequestedIntegrationEventHandler : IIntegrationEventHandler<NotificationRequestedIntegrationEvent>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<NotificationRequestedIntegrationEventHandler> _logger;
        private readonly INotificationIntegrationEventService _notificationIntegrationEventService;
        public NotificationRequestedIntegrationEventHandler(NotificationContext notificationContext,ILogger<NotificationRequestedIntegrationEventHandler> logger,
             INotificationIntegrationEventService notificationIntegrationEventService)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _notificationIntegrationEventService = notificationIntegrationEventService;
        }
        
        //Handler will consume integration event, which will save request to NotificationRequest table.
        public async Task Handle(NotificationRequestedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                //Save Notification Request into NotificationRequest table
                NotificationRequest notificationRequest = new NotificationRequest()
                {
                    EntityID = @event.EntityID,
                    ComChannel = @event.ComChannel.Name, //"test@gmail.com"                 
                    TopicID = @event.TopicID,
                    RequestMessageData = "Custom data from requestor",
                    NotificationStage = "Pending"
                };

                await _notificationContext.NotificationRequests.AddAsync(notificationRequest);
                await _notificationContext.SaveChangesAsync();              
                

            }
        }
    }
}
