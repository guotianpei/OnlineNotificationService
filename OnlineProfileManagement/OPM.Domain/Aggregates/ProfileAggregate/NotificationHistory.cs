using OPM.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    public class NotificationHistory:Entity,IAggregateRoot
    {
        public Guid TrackingID { get; set; }
        public Guid UserID { get; set; }
        public string ComChannel { get; set; }
        public string Recipient { get; set; }
        public string MessageBody { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
