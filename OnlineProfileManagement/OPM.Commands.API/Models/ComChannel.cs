using System;
using System.Runtime.Serialization;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Commands.API.Models
{
    public class ComChannel
    {
        public ComChannelTypes Types { get; set; }
        public int ComChannelId { get; set; }

        public string Value { get; set; }

        public bool Enabled { get; set; }

        public int Preference { get; set; }

        //public ComChannelStatus Status { get; set; }
    }
}
