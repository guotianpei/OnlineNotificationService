using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.Events;


namespace OPM.Commands.API.DomainEventHandler
{
    public class ComChannelAddedDomainEventHandler: IAsyncNotificationHandler<ComChannelAddedDomainEvent>
    {
        public ComChannelAddedDomainEventHandler()
        {
        }

        
        public async Task Handle(ComChannelAddedDomainEvent notification, CancellationToken cancellationToken)
        {
           
            throw new NotImplementedException();
        }
    }
}
