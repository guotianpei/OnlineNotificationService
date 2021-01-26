using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONP.Domain.Models;
using MMS.EventBus.Events;


namespace NotificationProcessor.API.IntegrationEvents.Events
{
    public class TemplateUpdatedIntegrationEvent: IntegrationEvent
    {
        //This integration event define template updated event
        public Guid ID { get; set; }

        public string TopicName { get; set; }

        public ComChannelTypes ComChannel { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string TemplateFile { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime TerminateDate { get; set; }
    }
}
