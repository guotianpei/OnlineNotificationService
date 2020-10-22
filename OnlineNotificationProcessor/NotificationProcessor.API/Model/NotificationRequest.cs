using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.Model
{
    public class NotificationRequest
    {
        public Guid ID { get; set; }
        public string EntityID { get; set; }
        public string ComChannel { get; set; }
        public string RequestMessageData { get; set; }
        public int TopicID { get; set; }
        public string NotificationStage { get; set; }
        public DateTime RequestDatetime { get; set; }

    }
}
