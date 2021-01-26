using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

using ONP.Domain.Models;

namespace ONP.Domain.Events
{
    //This event hold info for raising side effect once complete process for Notification request 
    public class RequestCompletedDomainEvent:  INotification
    {
        
        public Guid TrackingID { get; set; }
        //public NotificationStageEnum NotificationStage { get; set; }
        public string ResponseCode { get; set; }
        public Exception ResponseDesc { get; set; }
       // public List<NotificationResponse> BatchRequestResults { get; set; }

        public RequestCompletedDomainEvent(Guid trackingId, string responseCode, Exception desc)
        {
            TrackingID = trackingId;
            ResponseCode = responseCode;
            ResponseDesc = desc;

        }



    }
}
