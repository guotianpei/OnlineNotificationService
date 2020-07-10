using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPM.Domain.SeekWork;

namespace OPM.Domain.Aggregates.DistributionGroupsAggregate
{
    public class DistributionGroup : Entity, IAggregateRoot
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupMapping { get; set; }
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public DistributionGroup()
        {
        }
    }
}
