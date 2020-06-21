using MMS.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPM.Commands.API.IntegrationEvents
{
    public class ProfileIntegrationEventService : IProfileIntegrationEventService
    {
        public Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }

        public Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
