using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OPM.Queries.API.Queries;
using OPM.Infrastructure.Repositories;
using OPM.ONP.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;
using OPM.Queries.API.Models;
using System.Collections.Generic;
using OPM.Infrastructure.Repositories.QueryRequests;

namespace OPM.Queries.API.QueryHandlers
{
    public class GetProfileComChannelQueryHandler : IRequestHandler<GetProfileComChannelQuery, ProfileViewModel>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILogger<GetProfileComChannelQueryHandler> _logger;
        private readonly IMediator _mediator;


        public GetProfileComChannelQueryHandler(IMediator mediator,
            IProfileRepository profileRepository,
            ILogger<GetProfileComChannelQueryHandler> logger)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<ProfileViewModel> Handle(GetProfileComChannelQuery query, CancellationToken cancellationToken)
        {
            ProfileComChannelRequest comChannelRequest = new ProfileComChannelRequest();
            comChannelRequest.EntityIDs = query.EntityIDs;
            comChannelRequest.ComChannel = query.Comchannel;
            comChannelRequest.IsActive = query.IsActive;

            var returnComChannels = await _profileRepository.GetProfileComChannelByIDs(comChannelRequest);

            if (returnComChannels != null)
            {
                List<ProfileComChannelView> profileComChannelViewList = new List<ProfileComChannelView>();

                foreach (var comChannel in returnComChannels)
                {
                    profileComChannelViewList.Add(new ProfileComChannelView()
                    {
                        Type = (comChannel.ChannelType != null) ? comChannel.ChannelType.Name : string.Empty,
                        Value = comChannel.Value,
                        ComChannel = comChannel.ComChannel,
                        Preference = comChannel.Preference,
                        TermDate = comChannel.TermDate,
                        Status = (comChannel.Status != null) ? comChannel.Status.Name : string.Empty                        

                    }); 
                }

                return new ProfileViewModel()
                { 
                    ProfileComChannels = profileComChannelViewList,
                };
            }
            else
                return null;
        }
    }

}
