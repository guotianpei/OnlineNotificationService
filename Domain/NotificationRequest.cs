using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class NotificationRequest
    {
        //Rachel: GUID, instead of int ID, and also can we generate ID in DB, instead of in the request?
        public Guid ID { get; set; }

        public string EntityID { get; set; }

        public string ComChannel { get; set; }

        public string RequestMessageData { get; set; }

        public int TopicID { get; set; }

        public string NotificationStage { get; set; }
        public DateTime RequestDatetime { get; set; }//Rachel: will be default as current date

    }
}
