using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Events;
using NotificationProcessor.API.Model;

namespace NotificationProcessor.API.IntegrationEvents
{
    public class NotificationRequestedIntegrationEvent: IntegrationEvent
    {
        public Guid TrackingID { get; set; }

        public string EntityID { get; set; }

        public ComChannelTypes ComChannel { get; set; }

        public string RequestMessageData { get; set; }

        public int TopicID { get; set; }

    }
}
