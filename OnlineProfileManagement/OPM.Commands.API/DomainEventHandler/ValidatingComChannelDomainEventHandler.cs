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
    public class ValidatingComChannelDomainEventHandler: IAsyncNotificationHandler<ValidatingComChannelDomainEvent>
    {

        public ValidatingComChannelDomainEventHandler()
        { 
        }

        public async Task Handle(ValidatingComChannelDomainEvent notification, CancellationToken cancellationToken)
        {
            //TBD- Send validation email or TEXT based on channel type.
            throw new NotImplementedException();
        }
    }
}
