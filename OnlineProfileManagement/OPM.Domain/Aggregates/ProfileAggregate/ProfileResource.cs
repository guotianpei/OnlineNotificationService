using OPM.Domain.SeekWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    public class ProfileResource:Entity, IAggregateRoot
    {
        public string ResourceName { get; set; }
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }
    }
}
