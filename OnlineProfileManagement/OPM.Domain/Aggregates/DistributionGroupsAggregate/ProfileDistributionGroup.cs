using OPM.Domain.SeekWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPM.Domain.Aggregates.DistributionGroupsAggregate
{
    public class ProfileDistributionGroup :Entity, IAggregateRoot
    {
        public int GroupID { get; set; }
        public Guid UserID { get; set; }
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public DistributionGroup DistributionGroup { get; set; }
        public ProfileDistributionGroup()
        {
        }
    }
}
