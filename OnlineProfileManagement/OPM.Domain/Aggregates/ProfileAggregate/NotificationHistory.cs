using OPM.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    [Table("NotificationHistory")]
    public class NotificationHistory:Entity,IAggregateRoot
    {
        public Guid TrackingID { get; set; }
        public Guid UserID { get; set; }
        [Required]
        public string ComChannel { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string MessageBody { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime NotificationDate { get; set; }
    }
}
