using System;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Commands.API.Commands;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OPM.Infrastructure.Repositories;
using OPM.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;
using OPM.Commands.API.IntegrationEvents;
using OPM.Commands.API.IntegrationEvents.Events;
using System.Linq;

namespace OPM.Commands.API.CommandHandlers
{
    public class CreateEntityProfileCommandHandler : IRequestHandler<CreateEntityProfileCommand, bool>
    {

        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<CreateEntityProfileCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IProfileIntegrationEventService _profileIntegrationEventService;
        //public CreateEntityProfileCommandHandler()
        //{

        //}
        public CreateEntityProfileCommandHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<CreateEntityProfileCommandHandler> logger,IProfileIntegrationEventService profileIntegrationEventService)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _profileIntegrationEventService = profileIntegrationEventService ?? throw new ArgumentNullException(nameof(profileIntegrationEventService));
        }

        public async Task<bool> Handle(CreateEntityProfileCommand request, CancellationToken cancellationToken)
        {
            // TO-DO: Add any Integration event

            var entityRegisteredIntegrationEvent = new EntityRegisteredIntegrationEvent(request.EntityId, request.EntityName, request.EntityType, request.FirstName, request.LastName, Program.AppName, request.RequestID, request.ComChannels);
            await _profileIntegrationEventService.AddAndSaveEventAsync(entityRegisteredIntegrationEvent);

            // Add/Update the Profile AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Profile Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate
            var profile = new EntityProfile(request.EntityId, request.EntityName, request.EntityType,request.FirstName, request.LastName, request.Status, request.ResourceID);

            foreach (var channel in request.ComChannels)
            {
                profile.AddOrUpdateProfileComChannel(string.Empty,channel.ComChannelTypes, channel.Value, channel.Enabled, channel.Preference);
            }

            //_logger.LogInformation("----- Creating EntityProfile - EntityProfile: {@profile}", profile);
            await _profileRepository.Add(profile);
            return await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }



        // Use for Idempotency in Command process
        public class CreateEntityProfileIdentifiedCommandHandler : IdentifiedCommandHandler<CreateEntityProfileCommand, bool>
        {
            public CreateEntityProfileIdentifiedCommandHandler(
                IMediator mediator,
                IRequestManager requestManager,
                ILogger<IdentifiedCommandHandler<CreateEntityProfileCommand, bool>> logger)
                : base(mediator, requestManager, logger)
            {
            }

            protected override bool CreateResultForDuplicateRequest()
            {
                return true;                // Ignore duplicate requests for creating order.
            }
        }
    }
}
