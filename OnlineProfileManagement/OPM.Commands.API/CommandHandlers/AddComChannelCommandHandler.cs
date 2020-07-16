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

namespace OPM.Commands.API.CommandHandlers
{
    public class AddComChannelCommandHandler: IRequestHandler<AddComChannelCommand, bool>
    {

        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<AddComChannelCommandHandler> _logger;
        private readonly IMediator _mediator;



        public AddComChannelCommandHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<AddComChannelCommandHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<bool> Handle(AddComChannelCommand request, CancellationToken cancellationToken)
        {
            // TO-DO: Add any needed Integration event

            
            // Add/Update the Profile AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Profile Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate
            var profileToUpdate = await _profileRepository.GetAsync(request.EntityID);
            if(profileToUpdate == null)
            {
                return false;
            }
            foreach (var channel in request.ComChannels)
            {
                profileToUpdate.AddProfileComChannel(channel.ComChannelId,channel.Value, channel.Enabled, channel.Preference );
            }
            return await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                


        }


        // Use for Idempotency in Command process
        public class AddComChannelCommandIdentifiedCommandHandler : IdentifiedCommandHandler<AddComChannelCommand, bool>
        {
            public AddComChannelCommandIdentifiedCommandHandler(
                IMediator mediator,
                IRequestManager requestManager,
                ILogger<IdentifiedCommandHandler<AddComChannelCommand, bool>> logger)
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
