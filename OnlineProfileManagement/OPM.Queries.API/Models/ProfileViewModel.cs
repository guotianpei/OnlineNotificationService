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
        public List<ProfileComChannelView> profileComChannels { get; }
        public EntityProfileView EntityProfile { get; set; }

    }

    public class ProfileComChannelView
    {
        public ComChannelTypes Types { get; set; }
        public string Value { get; set; }
    }

    public class EntityProfileView
    {
        public string EntityName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
         
    }





}
