using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Net.Mail;
using ONP.Domain.Models;

namespace ONP.Domain.Events
{
    public class PublishFailedDomainEvent : INotification
    {
        public Guid TrackingID { get; set; }
        public string Recipient { get; set; }
        public ComChannelTypes ComChannel { get; set; }
        //public string Recipient { get; set; }
        //public DateTime TransactionDateTime { get; set; }
        public string MsgBody { get; set; }
        public NotificationStageEnum NotificationStage { get; set; }
        public SmtpStatusCode ResponseCode { get; set; }
        public Exception ResponseDesc { get; set; }

        public PublishFailedDomainEvent(Guid trackingID, string recipient, string msgBody, ComChannelTypes comChannel, 
            SmtpStatusCode responseCode, Exception desc)
        {
            TrackingID = trackingID;
            ResponseCode = responseCode;
            ComChannel = comChannel;
            ResponseDesc = desc;
            MsgBody = msgBody;
            ResponseDesc = desc;
             
            //.... any other info needed for additional actions needed.
        }
    }
}
