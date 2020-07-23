using System;
using System.Runtime.Serialization;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Commands.API.Models
{
    public class ComChannel
    {
        public string ComChannelType { get; set; }

        public string Value { get; set; }

        public bool Enabled { get; set; }

        public int Preference { get; set; }
    }
}
