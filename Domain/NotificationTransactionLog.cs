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

        //The stage cannot be set from public/outside of the object.
        //only thru the methods defined on the class, to avoid spaghetti code
        public NotificationState NotificationStage { get; private set; }

        public int ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public NotificationTransactionLog() { }

    } 
}
