using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;
using OPM.Commands.API.Models;

namespace OPM.Commands.API.IntegrationEvents.Events
{
    public class EntityRegisteredIntegrationEvent : IntegrationEvent
    {
        public string EntityID { get; set; }

        public string EntityName { get; set; }

        public string EntityType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RequestorApp { get; set; }

        public Guid RequestID { get; set; }

        public IEnumerable<ComChannel> ProfileComChannels { get; set; }

        public EntityRegisteredIntegrationEvent(string entityId, string entityName, string entityType, string firstName,
            string lastName, string requestorApp, Guid requestId, IEnumerable<ComChannel> comChannels)
        {
            EntityID = entityId;
            EntityName = entityName;
            EntityType = entityType;
            FirstName = firstName;
            LastName = lastName;
            RequestorApp = requestorApp;
            RequestID = requestId;
            ProfileComChannels = comChannels;

        }

    }
}
