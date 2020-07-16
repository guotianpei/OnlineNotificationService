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
        //public List<ProfileComChannel> profileComChannels { get; }
        public EntityProfile EntityProfile { get; set; }

    }

    public class ProfileComChannel
    {
    }
}
