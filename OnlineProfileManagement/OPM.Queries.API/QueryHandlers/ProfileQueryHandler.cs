using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OPM.Queries;
using OPM.Queries.API.Queries;

namespace OPM.Queries.API.QueryHandlers
{
    public class ProfileQueryHandler : IRequestHandler<ProfileQueries, string>
    {
        public ProfileQueryHandler()
        {
        }

        Task<string> IRequestHandler<ProfileQueries, string>.Handle(ProfileQueries request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
