using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Commands.API.Models;

namespace OPM.Commands.API.IntegrationEvents.Events
{
    public class ComChannelAddedIntegrationEvent : IntegrationEvent
    {
        public string FirstName { get; }

        public string LastName { get;}

        public IEnumerable<ComChannel> ComChannels { get; }

        public string Status { get;}

       

        public ComChannelAddedIntegrationEvent(string firstName, 
            string lastName, IEnumerable<ComChannel> comChannels, string status)
        {
            FirstName = firstName;
            LastName = lastName;
            ComChannels = comChannels;
            Status = status;

        }
    }
}
