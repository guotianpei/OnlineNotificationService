using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.Repositories.QueryRequests
{
    public class ProfileComChannelRequest
    {
        public List<ProfilEntityRequest> ListEntityIDs { get; set; }
        public string EntityIDs { get; set; }
        public string EntityID { get; set; }
        public string ComChannel { get; set; }
        public bool Enabled { get; set; }

    }

    public class ProfilEntityRequest
    {
        public string EntityID { get; set; }
    }
}
