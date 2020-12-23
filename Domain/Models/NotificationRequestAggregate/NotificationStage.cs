using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONP.Domain
{
    public class NotificationStage : Enumeration
    {
        //Initial notification request received.
        public static NotificationStage RequestReceived = new NotificationStage(0, nameof(RequestReceived).ToLowerInvariant()); 
        //RequestProcessing = 1, //Notification Background processor retrieve initial request, compose message body and retrieve contact details.
        public static NotificationStage RequestProcessing = new NotificationStage(1, nameof(RequestProcessing).ToLowerInvariant());
        //ToPublish = 2, // Notification Composition has been completed, to send to corresponding notification processor depends on type of communication channel.
        public static NotificationStage ToPublish = new NotificationStage(2, nameof(ToPublish).ToLowerInvariant());
        //Publishing = 3, // corresponding notification processor sending Email/SMS/SM notification
        public static NotificationStage Publishing = new NotificationStage(3, nameof(Publishing).ToLowerInvariant());
        //Published = 4, // Success
        public static NotificationStage Published = new NotificationStage(4, nameof(Published).ToLowerInvariant());
        //PublishFailed = 5, // Failed.
        public static NotificationStage PublishFailed = new NotificationStage(5, nameof(PublishFailed).ToLowerInvariant());
        
        //Original request completed: either sucess or fail to be write into original Notification request
        public static NotificationStage Completed = new NotificationStage(6, nameof(Completed).ToLowerInvariant());


        public NotificationStage(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<NotificationStage> List() =>
               new[] { RequestReceived };


 }
}
