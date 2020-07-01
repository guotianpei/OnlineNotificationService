using OPM.Domain.SeekWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPM.Domain.Aggregates.DistributionGroupsAggregate
{
    [Table("ProfileDistributionGroup")]
    public class ProfileDistributionGroup :Entity, IAggregateRoot
    {
        [ForeignKey("DistributionGroup")]
        public int GroupID { get; set; }
        public Guid UserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public DistributionGroup DistributionGroup { get; set; }

        public ProfileDistributionGroup()
        {
        }
    }
}
