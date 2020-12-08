using Domain;
using Microsoft.Extensions.Logging;
using MMS.EventBus.Abstractions;
using Infrastructure;
using NotificationProcessor.API.IntegrationEvents.Events;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents
{
    public class TemplateUpdatedIntegrationEventHandler : IIntegrationEventHandler<TemplateUpdatedIntegrationEvent>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<TemplateUpdatedIntegrationEventHandler> _logger;
        private readonly INotificationIntegrationEventService _notificationIntegrationEventService;
        public TemplateUpdatedIntegrationEventHandler(NotificationContext notificationContext, ILogger<TemplateUpdatedIntegrationEventHandler> logger,
             INotificationIntegrationEventService notificationIntegrationEventService)
        {
            _notificationContext = notificationContext;
            _logger = logger;
            _notificationIntegrationEventService = notificationIntegrationEventService;
        }

        //Handler will consume integration event, which will maintain local notification templates.
        public async Task Handle(TemplateUpdatedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                //Save Notification Request into NotificationRequest table
                NotificationTemplate templateToUpdate = new NotificationTemplate()
                {
                    //ID = @event.ID,
                    TopicName = @event.TopicName,
                    //ComChannel = !string.IsNullOrEmpty(@event.ComChannel.ToString()) ? ComChannelTypes.FromDisplayName<ComChannelTypes>(@event.ComChannel.ToString(),
                    From = @event.From,
                    Subject = @event.Subject,
                    TemplateFile = @event.TemplateFile,
                    EffectiveDate = @event.EffectiveDate,
                    TerminateDate=@event.TerminateDate,

                };

                //await _notificationContext.NotificationTransactionLogs.AddAsync(notificationLog);
                await _notificationContext.NotificationTemplates.AddAsync(templateToUpdate);
                await _notificationContext.SaveChangesAsync();
            }
        }
    }
}
