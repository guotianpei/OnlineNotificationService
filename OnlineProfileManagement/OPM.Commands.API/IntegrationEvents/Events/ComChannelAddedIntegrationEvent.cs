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

        public string ComChannelType { get; }

        public string ComChannelValue { get; }


        public ComChannelAddedIntegrationEvent(string firstName, 
            string lastName,  string comChannelType, string value)
        {
            FirstName = firstName;
            LastName = lastName;
            ComChannelType = comChannelType;
            ComChannelValue = value;
             
        }
    }
}
