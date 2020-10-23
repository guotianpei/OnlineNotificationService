﻿using Microsoft.Extensions.Logging;
using MMS.EventBus.Abstractions;
using NotificationProcessor.API.Infrastructure;
using NotificationProcessor.API.IntegrationEvents.Events;
using NotificationProcessor.API.Model;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents.Handlers
{
    public class EntityRegisteredIntegrationEventHandler : IIntegrationEventHandler<EntityRegisteredIntegrationEvent>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ILogger<EntityRegisteredIntegrationEventHandler> _logger;
        private readonly INotificationIntegrationEventService _notificationIntegrationEventService;
        public EntityRegisteredIntegrationEventHandler(NotificationContext notificationContext, ILogger<EntityRegisteredIntegrationEventHandler> logger,
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

                //Save Notification Request into NotificationRequest table
                EntityProfile profileToUpdate = new EntityProfile()
                {
                    EntityID = @event.EntityID,
                    EntityName = @event.EntityName,
                    EntityType = @event.EntityType,
                    FirstName = @event.FirstName,
                    LastName=@event.LastName,
                    Active=@event.Active,
                    Email =@event.Email,
                    SMS=@event.SMS,
                    SecureMassage=@event.SecureMassage                    
                };

                //await _notificationContext.NotificationTransactionLogs.AddAsync(notificationLog);
                await _notificationContext.EntityProfiles.AddAsync(profileToUpdate);
                await _notificationContext.SaveChangesAsync();
            }
    }
}