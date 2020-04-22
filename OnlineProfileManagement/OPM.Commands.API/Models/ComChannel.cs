using System;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Commands.API.Models
{
    public class ComChannel
    {

            public ComChannelTypes Types { get; set; }

            public string Value { get; set; }


            public bool Enabled { get; set; }


            public int Preference { get; set; }


            public ComChannelStatus Status { get; set; }
        
    }
}
