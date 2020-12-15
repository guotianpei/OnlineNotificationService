using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class NotificationRequest
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
        public NotificationState NotificationStage { get; private set; }

        public DateTime RequestDatetime { get; set; }//Rachel: will be default as current date. 
        //Mallika: No, it should be from the request/intergration event, not current date/time.



    }
}
