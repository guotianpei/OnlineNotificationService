using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPM.Domain.SeekWork;

namespace OPM.Domain.Aggregates.DistributionGroupsAggregate
{
    public class DistributionGroup : Entity, IAggregateRoot
    {
        [Required]
        public string GroupName { get; set; }
        [Required]
        public string GroupDescription { get; set; }
        [Required]
        public string GroupMapping { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EffDate { get; set; }
        public DateTime? TermDate { get; set; }

        public DistributionGroup()
        {
        }
    }
}
