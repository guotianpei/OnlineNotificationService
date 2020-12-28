using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

using ONP.Domain.Models;

namespace ONP.Domain
{
    //This event hold info for raising side effect once complete process for Notification request 
    public class RequestCompletedDomainEvent:  INotification
    {
        public Guid TrackingID { get; set; }
        //public string EntityID { get; set; }
        //public string comChannel { get; set; }
        //public string Recipient { get; set; }
        //public DateTime TransactionDateTime { get; set; }
        public NotificationStageEnum NotificationStage { get; set; }
        public int ResponseCode { get; set; }

        public RequestCompletedDomainEvent(Guid trackingID,  int responseCode )
        {
            TrackingID = trackingID;
            //NotificationStage = stage;
            ResponseCode = responseCode;
            //.... any other info needed for additional actions needed.
        }



    }
}
