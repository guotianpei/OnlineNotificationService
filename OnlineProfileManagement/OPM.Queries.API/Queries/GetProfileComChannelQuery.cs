using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPM.Queries.API.Models;

namespace OPM.Queries.API.Queries
{
    public class GetProfileComChannelQuery : QueryBase<ProfileViewModel>
    {
        //public string EntityID { get; set; }
        public string EntityIDs { get; set; }
        public string Comchannel { get; set; }
        public bool IsActive { get; set; }

        public GetProfileComChannelQuery()
        {
        }

    }
}
