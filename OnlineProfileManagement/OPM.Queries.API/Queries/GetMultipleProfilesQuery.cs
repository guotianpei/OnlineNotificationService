using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPM.Queries.API.Models;

namespace OPM.Queries.API.Queries
{
    public class GetMultipleProfilesQuery : QueryBase<ProfileViewModel>
    {
        public List<string> entityIDs { get; set; }

        public GetMultipleProfilesQuery()
        {
        }

        public GetMultipleProfilesQuery(List<string> entityIds)
        {
            entityIDs = entityIds;
        }
    }
}
