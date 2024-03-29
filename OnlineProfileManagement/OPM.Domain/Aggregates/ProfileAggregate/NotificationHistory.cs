﻿using OPM.Domain.SeekWork;
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
        public Guid EntityID { get; set; }
        public string ComChannel { get; set; }
        public string Recipient { get; set; }
        public string TopicID { get; set; }
        public string MessageBody { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationStage { get; set; }
        //public EntityProfile EntityProfile { get; set; }
    }
}
