using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using ONP.Domain.Seedwork;



namespace ONP.Domain.Models
{
    public class NotificationStageEnum : Enumeration
    {
        //Initial notification request received.
        public static NotificationStageEnum RequestReceived = new NotificationStageEnum(0, nameof(RequestReceived).ToLowerInvariant()); 
        //RequestProcessing = 1, //Notification Background processor retrieve initial request, compose message body and retrieve contact details.
        public static NotificationStageEnum RequestProcessing = new NotificationStageEnum(1, nameof(RequestProcessing).ToLowerInvariant());
        //ToPublish = 2, // Notification Composition has been completed, to send to corresponding notification processor depends on type of communication channel.
        public static NotificationStageEnum ToPublish = new NotificationStageEnum(2, nameof(ToPublish).ToLowerInvariant());
        //Publishing = 3, // corresponding notification processor sending Email/SMS/SM notification
        public static NotificationStageEnum Publishing = new NotificationStageEnum(3, nameof(Publishing).ToLowerInvariant());
        //Published = 4, // Success
        public static NotificationStageEnum Published = new NotificationStageEnum(4, nameof(Published).ToLowerInvariant());
        //PublishFailed = 5, // Failed.
        public static NotificationStageEnum PublishFailed = new NotificationStageEnum(5, nameof(PublishFailed).ToLowerInvariant());
        
        //Original request completed: either sucess or fail to be write into original Notification request
        public static NotificationStageEnum Completed = new NotificationStageEnum(6, nameof(Completed).ToLowerInvariant());


        public NotificationStageEnum(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<NotificationStageEnum> List() =>
               new[] { RequestReceived, RequestProcessing, ToPublish, Publishing, Published, PublishFailed, Completed };


 }
}
