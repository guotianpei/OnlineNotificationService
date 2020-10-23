using MMS.EventBus.Events;
using NotificationProcessor.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.IntegrationEvents.Events
{
    public class EntityRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid TrackingID { get; set; }

        public string EntityID { get; set; }

        //public ComChannelTypes ComChannel { get; set; }
        public string ComChannel { get; set; }

        public string Recipient { get; set; }

        public int TopicID { get; set; }
    }
}
