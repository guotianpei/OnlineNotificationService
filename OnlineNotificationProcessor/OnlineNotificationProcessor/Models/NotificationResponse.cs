using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineNotificationProcessor.Models
{
    public class NotificationResponse
    {
        public string ID { get; set; }
        public NotificationStage NotificationStage { get; set; }
        public string ResponseMessage { get; set; }
        public Exception Error { get; set; }
    }

    public enum NotificationStage
    {
        Pending,
        Success,
        Failed
    }
}
