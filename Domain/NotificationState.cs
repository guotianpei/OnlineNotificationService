using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    public class NotificationState : Enumeration
    {
        //Initial notification request received.
        public static NotificationState RequestReceived = new NotificationState(0, nameof(RequestReceived).ToLowerInvariant()); 
        //RequestProcessing = 1, //Notification Background processor retrieve initial request, compose message body and retrieve contact details.
        public static NotificationState RequestProcessing = new NotificationState(1, nameof(RequestProcessing).ToLowerInvariant());
        //ToPublished = 2, // Notification Composition has been completed, has been sent to corresponding notification processor depends on type of communication channel.
        public static NotificationState ToPublished = new NotificationState(2, nameof(ToPublished).ToLowerInvariant());
        //InProgress = 3, // Before corresponding notification processor sending Email/SMS/SM notification
        public static NotificationState InProgress = new NotificationState(3, nameof(InProgress).ToLowerInvariant());
        //Published = 4, // Success
        public static NotificationState Published = new NotificationState(4, nameof(Published).ToLowerInvariant());
        //PublishedFailed = 5, // Failed.
        public static NotificationState PublishedFailed = new NotificationState(5, nameof(PublishedFailed).ToLowerInvariant());
        
        //Original request completed: either sucess or fail to be write into original Notification request
        public static NotificationState Completed = new NotificationState(6, nameof(Completed).ToLowerInvariant());


        public NotificationState(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<NotificationState> List() =>
               new[] { RequestReceived };


 }
}
