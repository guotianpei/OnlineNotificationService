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
            //TBD- Send validation email or TEXT based on channel type.
            var type = notification.ComChannel.ChannelType;
            if (type == ComChannelTypes.Email || type == ComChannelTypes.TEXT)
            {
                //Publish msg to MSG Publisher for email or text for verification.
                //Do not send in the service as template maintained in cental Template Management service
                //Raise ComChannelAddedIntegrationEvent send integration event to event bus.

            }


        }
    }
}
