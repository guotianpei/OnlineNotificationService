using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.Domain.Models
{
    public class NotificationResponse
    {
        public Guid TrackingID { get; set; }
        public string ResponseCode { get; set; }
       // public string ResponseMessage { get; set; }
        public Exception Error { get; set; }
    }
}
