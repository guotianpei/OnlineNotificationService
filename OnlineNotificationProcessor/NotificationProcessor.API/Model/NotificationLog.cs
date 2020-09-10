using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationProcessor.API.Infrastructure;

 

namespace NotificationProcessor.API.Model
{
    public class NotificationLog
    {
        public Guid TrackingID { get; set; }

        public string EntityID { get; set; }

        //public ComChannelTypes ComChannel { get; set; }
        public string ComChannel { get; set; }

        public string Recipient { get; set; }

        public int TopicID { get; set; }

        public string MessageBody { get; set; }

        public DateTime NotificationDate { get; set; }

        //public NotificationStateEnum NotificationStage { get; set; }
        public string NotificationStage { get; set; }
        public NotificationLog() { }

    } 
}
