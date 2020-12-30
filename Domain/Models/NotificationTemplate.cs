using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Domain.Models
{
    public class NotificationTemplate : Entity, IAggregateRoot
    {
        public int ID { get; set; }
        public string TopicName { get; set; }
        public string ComChannel { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string TemplateFile { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime TerminateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }

    }
}
