using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;
using NotificationProcessor.API.Model;

namespace NotificationProcessor.API.IntegrationEvents
{
    public class NotificationRequestIntegrationEvent: IntegrationEvent
    {
        public Guid TrackingID { get; set; }

        public Guid EntityID { get; set; }

        public ComChannelTypes ComChannel { get; set; }

        public string Recipient { get; set; }

        public int TopicID { get; set; }
    }
}
