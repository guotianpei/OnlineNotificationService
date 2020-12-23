using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ONP.Domain
{
    public class NotificationRequest : Entity
    {
        //Rachel: GUID, instead of int ID, and also can we generate ID in DB, instead of in the request?
        //Mallika: It should be from requestor, for tracking purpose. So requestor can track the request from their end.
        public Guid ID { get; set; }

        public string EntityID { get; set; }

        public string ComChannel { get; set; }

        public string RequestMessageData { get; set; }

        public int TopicID { get; set; }

        //The stage cannot be set from public/outside of the object.
        //only thru the methods defined on the class, to avoid spaghetti code
        public NotificationStage NotificationStage { get; private set; }
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

       

        public NotificationRequest(Guid id, string entityId, string comChannel, string requestMessageDate, int topicId, string requestDateTime): this()
        {
            ID = id;
            EntityID = entityId;
            ComChannel = comChannel;
            RequestMessageData = requestMessageDate;
            TopicID = topicId;
            RequestDatetime = requestDateTime;
            _notificationStageId = NotificationStage.RequestReceived.Id;

        }

        public void SetRequestProcessingStage()
        {
            //Set Request stage=1; and initiate/to create  transaction Log
            if(_notificationStageId!= NotificationStage.RequestReceived.Id)
            {
                StageChangeException(NotificationStage.RequestProcessing);
            }
            
            _notificationStageId = NotificationStage.RequestProcessing.Id;

            CreateTransactionLog();
        }

        private void CreateTransactionLog()
        {
            var log = new NotificationTransactionLog(ID, ComChannel, TopicID);
            _transactionLog = log;
        }       
     
       

        //ToPublish = 2, // Notification Composition has been completed, 
        //to send to corresponding notification processor depends on type of communication channel.
        //Need to update Recipient, MessageBody and NotificationStage
        public void SetToPublishStage( string recipient, string messageBody)
        {
            //check current stage of transaction log.
            if(_transactionLog.NotificationStage.Id!=NotificationStage.RequestProcessing.Id)
            {
                StageChangeException(NotificationStage.ToPublish);
            }
            _transactionLog.NotificationStage = NotificationStage.ToPublish;
            _transactionLog.Recipient = recipient;
            _transactionLog.MessageBody = messageBody;
        }

        //Publishing = 3, //  corresponding notification processor sending Email/SMS/SM notification
        public void SetPublishingStage()
        {
            //check current stage of transaction log.
            if (_transactionLog.NotificationStage.Id != NotificationStage.ToPublish.Id)
            {
                StageChangeException(NotificationStage.Publishing);
            }
            _transactionLog.NotificationStage = NotificationStage.Publishing;           
        }

        //Completed = 5, // Either Success or failed depends on responseCode
        public void SetCompletedStage(string responseCode, string responseDescription)
        {
            //check current stage of transaction log.
            if (_transactionLog.NotificationStage.Id != NotificationStage.Publishing.Id)
            {
                StageChangeException(NotificationStage.Published);
            }
            //Get the list of FailureNotifyCodes and compare, to determine the stage and further action.
            if(GetfailureNotifyingCodes().Contains(responseCode))
            {
                //PublishFailed = 5, // Failed.
                _transactionLog.NotificationStage = NotificationStage.PublishFailed;
                //Raise PublishFailedDomainEvent   as side effect.
                AddDomainEvent(new PublishFailedDomainEvent(ID, responseCode));
            }
            else
            {
                //Published = 4, // Success
                _transactionLog.NotificationStage = NotificationStage.Published;
            }
            
            _transactionLog.ResponseCode = responseCode;
            _transactionLog.ResponseDescription = responseDescription;
            //Update stage in NotificationRequest
            NotificationStage = NotificationStage.Completed;
        }

       

        private void StageChangeException (NotificationStage stageToChange)
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
