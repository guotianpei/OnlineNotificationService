﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Models
{
    public class NotificationData
    {
        public Guid TrackingID { get; set; } //Notification request id       
        public string ComChannel { get; set; }
        public string RequestMessageData { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string MobileNo { get; set; }
        public string RequestDateTime { get; set; }
    }
}
