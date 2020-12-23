using System;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Commands.API.Commands;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OPM.Infrastructure.Repositories;
using OPM.ONP.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;

namespace OPM.Commands.API.CommandHandlers
{
    public class AddOrUpdateComChannelCommandHandler: IRequestHandler<AddOrUpdateComChannelCommand, bool>
    {

        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<AddOrUpdateComChannelCommandHandler> _logger;
        private readonly IMediator _mediator;

        public AddOrUpdateComChannelCommandHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<AddOrUpdateComChannelCommandHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(AddOrUpdateComChannelCommand request, CancellationToken cancellationToken)
        {
            var profileToUpdate = await _profileRepository.GetAsync(request.EntityID);
            foreach (var channel in request.ComChannels)
            { 
                profileToUpdate.AddOrUpdateProfileComChannel(request.EntityID, channel.ComChannelTypes, channel.Value, channel.Enabled, channel.Preference);
                       
            }
            return await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }


        // Use for Idempotency in Command process
        public class AddOrUpdateComChannelCommandIdentifiedCommandHandler : IdentifiedCommandHandler<AddOrUpdateComChannelCommand, bool>
        {
            public AddOrUpdateComChannelCommandIdentifiedCommandHandler(
                IMediator mediator,
                IRequestManager requestManager,
                ILogger<IdentifiedCommandHandler<AddOrUpdateComChannelCommand, bool>> logger)
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
