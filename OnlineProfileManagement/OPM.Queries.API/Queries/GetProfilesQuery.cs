using System;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OPM.Queries;

namespace OPM.Queries.API.Queries
{
    public class GetProfilesQuery : QueryBase<ProfileViewModel>
    {
        public List<string> entityIDs { get; set; }

        public GetProfilesQuery()
        {
        }

        public GetProfilesQuery(List<string> entityIds)
        {
            entityIDs = entityIds;
        }
    }
}