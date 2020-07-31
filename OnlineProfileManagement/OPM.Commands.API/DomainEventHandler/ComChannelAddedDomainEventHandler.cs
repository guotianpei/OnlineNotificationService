using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.Events;
using OPM.Commands.API.IntegrationEvents.Events;
using OPM.Commands.API.Models;
using OPM.Infrastructure.Repositories.Interfaces;
using OPM.Commands.API.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace OPM.Commands.API.DomainEventHandler
{
    public class ComChannelAddedDomainEventHandler: IAsyncNotificationHandler<ComChannelAddedDomainEvent>
    {
        private readonly IProfileRepository _profileRepository;

        private readonly IMediator _mediator;
        private readonly IProfileIntegrationEventService _profileIntegrationEventService;
        private readonly ILogger<ComChannelAddedDomainEventHandler> _logger;


        // Using DI to inject infrastructure persistence Repositories
        public ComChannelAddedDomainEventHandler(IMediator mediator,
            IProfileIntegrationEventService profileIntegrationEventService,
            IProfileRepository profileRepository,
            ILogger<ComChannelAddedDomainEventHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _profileIntegrationEventService = profileIntegrationEventService ?? throw new ArgumentNullException(nameof(profileIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
 


        public async Task Handle(ComChannelAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TBD- Send validation email or TEXT based on channel type.
            var type = notification.ComChannel.ChannelType;
            var comChannel = new ComChannel();
            comChannel.ComChannelType = notification.ComChannel.ChannelType.ToString();
            comChannel.Value = notification.ComChannel.Value;


            if (type == ComChannelTypes.Email || type == ComChannelTypes.TEXT)
            {
                //Publish msg to MSG Publisher for email or text for verification.
                //Do not send in the service as template maintained in cental Template Management service
                //Raise ComChannelAddedIntegrationEvent send integration event to event bus.
                var comChannelAddedIntegrationEvent = new ComChannelAddedIntegrationEvent(
                    notification.FName, 
                    notification.LName, 
                    notification.ComChannel.ChannelType.ToString(), 
                    notification.ComChannel.Value);
                await _profileIntegrationEventService.AddAndSaveEventAsync(comChannelAddedIntegrationEvent);
            }


        }
    }
}
