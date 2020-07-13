using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;

namespace OPM.Commands.API.IntegrationEvents.Events
{
    public class ComChannelAddedIntegrationEvent : IntegrationEvent
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TopicID { get; set; }
    }
}
