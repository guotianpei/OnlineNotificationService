using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;

namespace ONP.BackendProcessor.Models
{
    public class NotificationResponse
    {
        public Guid TrackingID { get; set; }
        public NotificationStageEnum NotificationStage { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public Exception Error { get; set; }
    }

   
}
