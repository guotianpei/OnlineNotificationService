using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONP.Domain
{
    public class FailureNotifyCodes
    {
        public int ID { get; set; }
        public string FailureCode { get; set; }
        public string NotificationRecipient { get; set; }
        public int TopicID { get; set; }
        public bool Active { get; set; }
        public DateTime LastUpdateDate { get; set; }

    }
}
