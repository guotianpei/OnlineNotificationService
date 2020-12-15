using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Domain.Events
{
    class NotificationRequestInProcessingDomainEvent : INotification
    {
        public Guid TrackingID { get; set; }
        //public string EntityID { get; set; }
        //public string comChannel { get; set; }
        //public string Recipient { get; set; }
        //public DateTime TransactionDateTime { get; set; }
        public NotificationState NotificationStage { get; set; }
        public int ResponseCode { get; set; }

        public NotificationRequestInProcessingDomainEvent(Guid trackingID, NotificationState stage, int responseCode)
        {
            TrackingID = trackingID;
            NotificationStage = stage;
            ResponseCode = responseCode;
            //.... any other info needed for additional actions needed.
        }
    }
}
