using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace ONP.Domain.Models
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

        public NotificationStageEnum NotificationStage { get;  set; }

        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public NotificationTransactionLog() { }

        public NotificationTransactionLog(Guid trackingId,  string comChannel, int topicId)            
        {
            TrackingID = trackingId;
            //EntityID = entityId;
            ComChannel = comChannel;
            //Recipient = recipient;
            TopicID = topicId;
            //RequestProcessing = 1, Notification Background processor retrieve initial request, 
            NotificationStage = NotificationStageEnum.RequestProcessing;
            //MessageBody = messageBody;
        }



    } 
}
