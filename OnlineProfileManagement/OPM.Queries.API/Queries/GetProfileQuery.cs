using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OPM.Queries;

namespace OPM.Queries.API.Queries
{
    public class GetProfileQuery : QueryBase<ProfileViewModel>
    {
        public string entityID { get; set; }

        public GetProfileQuery()
        {
        }

        public GetProfileQuery(string entityId)
        {
            entityID = entityId;
        }
    }
}