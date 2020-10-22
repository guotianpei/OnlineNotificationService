using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineNotificationProcessor.Models
{
    public class NotificationTemplate
    {
        public string ID { get; set; }
        public string TopicName { get; set; }
        public string ComChannel { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string TemplateFile { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
