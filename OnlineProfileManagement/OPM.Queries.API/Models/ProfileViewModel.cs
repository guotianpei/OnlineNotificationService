using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Queries.API.Models;

namespace OPM.Queries.API.Models
{
    public class ProfileViewModel
    {
        public List<ProfileComChannelView> ProfileComChannels { get; set; }
        public EntityProfileView EntityProfile { get; set; }
    }

    public class ProfileComChannelView
    {
        public string Type { get; set; }
        public string Value { get; set; }

        //New
        public int Preference { get; set; }
        public DateTime? TermDate { get; set; }

        public string ComChannel { get; set; }
        public string Status { get; set; }
        
    }

    public class EntityProfileView
    {
        public string EntityName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ResourceName { get; set; }
    }





}
