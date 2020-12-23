using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Models
{
    public class NotificationRequest
    {

        public string ID { get; set; }
        public string EntityID { get; set; }
        public string ComChannel { get; set; }
        public string RequestMessageData { get; set; }
        public string TopicID { get; set; }
        public string NotificationStage { get; set; }
        public string RequestDateTime { get; set; }
    }
}
