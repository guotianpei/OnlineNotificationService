using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class NotificationTransactionLog
    {
        public Guid TrackingID { get; set; }

        public string EntityID { get; set; }

        //public ComChannelTypes ComChannel { get; set; }
        public string ComChannel { get; set; }

        public string Recipient { get; set; }

        public int TopicID { get; set; }

        public string MessageBody { get; set; }

        public DateTime TransactionDateTime { get; set; }
        public int RetryCounts { get; set; }
        public string NotificationStage { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public NotificationTransactionLog() { }

    } 
}
