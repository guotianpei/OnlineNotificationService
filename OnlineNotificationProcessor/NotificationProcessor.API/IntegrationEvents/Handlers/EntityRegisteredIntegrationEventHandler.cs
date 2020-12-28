using ONP.Domain.Models;
using ONP.Infrastructure;
using Microsoft.Extensions.Logging;
using MMS.EventBus.Abstractions;
using NotificationProcessor.API.IntegrationEvents.Events;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents.Handlers
{
    public class EntityRegisteredIntegrationEventHandler : IIntegrationEventHandler<EntityRegisteredIntegrationEvent>
    {
        private readonly NotificationProcessorContext _notificationRequestContext;
        private readonly ILogger<EntityRegisteredIntegrationEventHandler> _logger;
        private readonly INotificationIntegrationEventService _notificationIntegrationEventService;
        public EntityRegisteredIntegrationEventHandler(NotificationProcessorContext notificationContext, ILogger<EntityRegisteredIntegrationEventHandler> logger,
             INotificationIntegrationEventService notificationIntegrationEventService)
        {
            _notificationRequestContext = notificationContext;
            _logger = logger;
            _notificationIntegrationEventService = notificationIntegrationEventService;
        }


        public async Task Handle(EntityRegisteredIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                //Save Notification Request into NotificationRequest table
                EntityProfile profileToUpdate = new EntityProfile()
                {
                    EntityID = @event.EntityID,
                    EntityName = @event.EntityName,
                    EntityType = @event.EntityType,
                    FirstName = @event.FirstName,
                    LastName = @event.LastName,
                    Active = true,
                    Email = (@event.ProfileComChannels != null && @event.ProfileComChannels.Count() > 0 && @event.ProfileComChannels.Any(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.Email.ToString())) ? @event.ProfileComChannels.SingleOrDefault(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.Email.ToString()).Value : "",
                    SMS = (@event.ProfileComChannels != null && @event.ProfileComChannels.Count() > 0 && @event.ProfileComChannels.Any(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.TEXT.ToString())) ? @event.ProfileComChannels.SingleOrDefault(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.TEXT.ToString()).Value : "",
                    SecureMassage = (@event.ProfileComChannels != null && @event.ProfileComChannels.Count() > 0 && @event.ProfileComChannels.Any(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.SecureMessage.ToString())) ? @event.ProfileComChannels.SingleOrDefault(c => c.ComChannelType.ToLowerInvariant() == ComChannelTypes.SecureMessage.ToString()).Value : "",
                    LastUpdateDateTime = DateTime.Now
                };

                //await _notificationContext.NotificationTransactionLogs.AddAsync(notificationLog);
                await _notificationRequestContext.EntityProfiles.Add(profileToUpdate);
                await _notificationRequestContext.SaveChangesAsync();
            }
        }
    }
}
