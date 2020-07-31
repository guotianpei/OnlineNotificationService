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
using System.Collections.Generic;

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

            if (profile !=null)
            {
                List<ProfileComChannelView> profileComChannelViewList = new List<ProfileComChannelView>();

                foreach(var comChannel in profile.ProfileComChannels)
                {
                    profileComChannelViewList.Add(new ProfileComChannelView()
                    {
                        Type = (comChannel.ChannelType != null) ? comChannel.ChannelType.Name : string.Empty,
                        Value = comChannel.Value
                    });
                }
                return new ProfileViewModel()
                {
                    ProfileComChannels = profileComChannelViewList,
                    EntityProfile = new EntityProfileView()
                    {
                        EntityName = profile.EntityName,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        ResourceName = (profile.ProfileResource != null) ? profile.ProfileResource.ResourceName : string.Empty
                    }
                };
            }
            return null;
        }
    }
}
