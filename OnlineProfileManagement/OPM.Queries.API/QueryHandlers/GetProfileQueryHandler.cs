using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OPM.Queries.API.Queries;
using OPM.Infrastructure.Repositories;
using OPM.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;
using OPM.Queries.API.Models;


namespace OPM.Queries.API.QueryHandlers
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileViewModel>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<GetProfileQueryHandler> _logger;
        private readonly IMediator _mediator;


        public GetProfileQueryHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<GetProfileQueryHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<ProfileViewModel> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _profileRepository.GetAsync(request.entityID);
            if(profile !=null)
            {
                //TO-DO: Mapping return to ProfileViewModel object
                //var profileViewModel =
                //return profileViewModel;
            }
            
            return null;
        }
    }
}
