using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineNotificationProcessor.Models
{
    public class NotificationLog
    {
        public Guid TrackingID { get; set; }
        public Guid EntityID { get; set; }
        public string ComChannel { get; set; }
        public string Recipient { get; set; }
        public string TopicID { get; set; }
        public string MessageBody { get; set; }
        public DateTime NotificationDate { get; set; }
        public int RetryCounts { get; set; }
        public int NotificationStatge { get; set; }
        public int ResponseCode { get; set; }
        public int ResponseDescription { get; set; }
    }
}
