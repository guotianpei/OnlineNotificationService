using MMS.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents
{
    public interface INotificationIntegrationEventService
    {
        Task SaveEventAndNotificationLogChangesAsync(IntegrationEvent evt);
    }
}
