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
    public class UpdateEntityProfileStatusCommandHandler : IRequestHandler<UpdateEntityProfileStatusCommand, bool>
    {

        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<UpdateEntityProfileStatusCommandHandler> _logger;
        private readonly IMediator _mediator;

        public UpdateEntityProfileStatusCommandHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<UpdateEntityProfileStatusCommandHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(UpdateEntityProfileStatusCommand request, CancellationToken cancellationToken)
        {
            var profileToUpdate = await _profileRepository.GetAsync(request.EntityId);
            if (profileToUpdate == null)
            {
                //TO-DO: log profile doesn't exist, 
                //return to API with error msg.
                return false;
            }
            profileToUpdate.UpdateProfileStatus(request.Status);
            return await _profileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        } 
 
    }
}
