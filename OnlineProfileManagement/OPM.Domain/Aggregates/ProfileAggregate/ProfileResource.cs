﻿using OPM.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    [Table("ProfileResource")]
    public class ProfileResource:Entity, IAggregateRoot
    {
        [Required]
        public string ResourceName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }

    }
}