using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using MMS.EventBus.Events;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MMS.IntegrationEventLogEF
{
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }
        public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
        {
            EventId = @event.Id;            
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Content = JsonConvert.SerializeObject(@event);
            EventState = EventStateEnum.NotPublished;
            State = "0";
            TimesSent = 0;
            TransactionId = transactionId.ToString();
        }

        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }

        public EventStateEnum EventState
        {
            get
            {
                return (!string.IsNullOrEmpty(State) && Enum.IsDefined(typeof(EventStateEnum), State)) ? (EventStateEnum)Enum.Parse(typeof(EventStateEnum), State) : EventStateEnum.NotPublished;
            }
            set { }
        }
        public string State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }
        public string TransactionId { get; private set; }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonConvert.DeserializeObject(Content, type) as IntegrationEvent;
            return this;
        }
    }
}
