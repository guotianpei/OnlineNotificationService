using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;

namespace OPM.Commands.API.IntegrationEvents
{
    interface IProfileIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
