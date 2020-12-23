using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Models
{
    public class NotificationTransactionLog
    {

        public string TrackingID { get; set; }
        public string EntityID { get; set; }
        public string ComChannel { get; set; }
        public string Recipient { get; set; }
        public string MessageBody { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int RetryCounts { get; set; }
        public string NotificationStatge { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }

    }
}
