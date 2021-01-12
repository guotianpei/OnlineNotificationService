using System;
using System.Collections.Generic;
using ONP.Domain.Seedwork;
using System.Threading.Tasks;

namespace ONP.Domain.Models
{
    public class NotificationRequest : Entity, IAggregateRoot
    {
        //Rachel: GUID, instead of int ID, and also can we generate ID in DB, instead of in the request?
        //Mallika: It should be from requestor, for tracking purpose. So requestor can track the request from their end.
        public Guid TrackingID { get; set; }

        public string EntityID { get; set; }

        public string ComChannel { get; set; }

        public string RequestMessageData { get; set; }

        public string Recipient { get; set; }

        public int TopicID { get; set; }

        //The stage cannot be set from public/outside of the object.
        //only thru the methods defined on the class, to avoid spaghetti code
        public NotificationStageEnum NotificationStage { get; private set; }
        private int _notificationStageId;

        public string RequestDatetime { get; set; }//Rachel: will be default as current date. 
                                                   //Mallika: No, it should be from the request/intergration event, not current date/time.

        //DDD Patterns comment
        //Using a private collection fields, better for DDD Aggregate's encapsulation
        //So transactionLog cannot be added from "outside of AggregateRoot" directly to the collection,
        //Can only be added thruough the method NotificationRequest.CreateTransactionLog to keep data consistency. 

        private NotificationTransactionLog _transactionLog;

        public NotificationTransactionLog NotificationTransactionLog => _transactionLog;

        protected NotificationRequest()
        {
            _transactionLog = new NotificationTransactionLog();
        }

       

        public NotificationRequest(Guid trackingID, string entityId, string comChannel, string requestMessageDate, int topicId, string requestDateTime): this()
        {
            TrackingID = trackingID;
            EntityID = entityId;
            ComChannel = comChannel;
            RequestMessageData = requestMessageDate;
            TopicID = topicId;
            RequestDatetime = requestDateTime;
            _notificationStageId = NotificationStageEnum.RequestReceived.Id;

        }

        //public void SetRequestProcessingStage()
        //{
        //    //Set Request stage=1; and initiate/to create  transaction Log
        //    if(_notificationStageId!= NotificationStageEnum.RequestReceived.Id)
        //    {
        //        StageChangeException(NotificationStageEnum.RequestProcessing);
        //    }
            
        //    _notificationStageId = NotificationStageEnum.RequestProcessing.Id;

        //    CreateTransactionLog();
        //}

        //private void CreateTransactionLog()
        //{
        //    var log= new NotificationTransactionLog(TrackingID, ComChannel, TopicID);
          
        //}       
     
       

        //ToPublish = 2, // Notification Composition has been completed, 
        //to send to corresponding notification processor depends on type of communication channel.
        //Need to update Recipient, MessageBody and NotificationStage
        public void SetToPublishStage( string recipient, string messageBody)
        {
            //check current stage of transaction log.
            if(_transactionLog.NotificationStage.Id!= NotificationStageEnum.RequestProcessing.Id)
            {
                StageChangeException(NotificationStageEnum.ToPublish);
            }
            _transactionLog.NotificationStage = NotificationStageEnum.ToPublish;
            _transactionLog.Recipient = recipient;
            _transactionLog.MessageBody = messageBody;
        }

        //Publishing = 3, //  corresponding notification processor sending Email/SMS/SM notification
        public void SetPublishingStage()
        {
            //check current stage of transaction log.
            if (_transactionLog.NotificationStage.Id != NotificationStageEnum.ToPublish.Id)
            {
                StageChangeException(NotificationStageEnum.Publishing);
            }
            _transactionLog.NotificationStage = NotificationStageEnum.Publishing;           
        }

        //Completed = 5, // Either Success or failed depends on responseCode
        public void SetCompletedStage(string responseCode, string responseDescription)
        {
            //check current stage of transaction log.
            if (_transactionLog.NotificationStage.Id != NotificationStageEnum.Publishing.Id)
            {
                StageChangeException(NotificationStageEnum.Published);
            }
            //Get the list of FailureNotifyCodes and compare, to determine the stage and further action.
            if(GetfailureNotifyingCodes().Contains(responseCode))
            {
                //PublishFailed = 5, // Failed.
                _transactionLog.NotificationStage = NotificationStageEnum.PublishFailed;
                //Raise PublishFailedDomainEvent   as side effect.
                AddDomainEvent(new PublishFailedDomainEvent(TrackingID, responseCode));
            }
            else
            {
                //Published = 4, // Success
                _transactionLog.NotificationStage = NotificationStageEnum.Published;
            }
            
            _transactionLog.ResponseCode = responseCode;
            _transactionLog.ResponseDescription = responseDescription;
            //Update stage in NotificationRequest
            NotificationStage = NotificationStageEnum.Completed;
        }

        

       

        private void StageChangeException (NotificationStageEnum stageToChange)
        {
            throw new ONPDomainException($"Cannot change request stage from {NotificationStage.Name} to {stageToChange.Name}.");
        }

        public List<string> GetfailureNotifyingCodes()
        {
            List<string> lst = new List<string>();
            return lst;
        }


    }
}
