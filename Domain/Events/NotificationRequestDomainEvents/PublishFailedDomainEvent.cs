using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ONP.Domain.Models;

namespace ONP.Domain
{
    public class PublishFailedDomainEvent : INotification
    {
        public Guid TrackingID { get; set; }
        //public string EntityID { get; set; }
        //public string comChannel { get; set; }
        //public string Recipient { get; set; }
        //public DateTime TransactionDateTime { get; set; }
        public NotificationStageEnum NotificationStage { get; set; }
        public string ResponseCode { get; set; }

        public PublishFailedDomainEvent(Guid trackingID, string responseCode)
        {
            TrackingID = trackingID;
            ResponseCode = responseCode;
            //.... any other info needed for additional actions needed.
        }
    }
}
