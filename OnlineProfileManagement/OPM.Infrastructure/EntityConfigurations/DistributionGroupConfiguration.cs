using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class DistributionGroupConfiguration :IEntityTypeConfiguration<DistributionGroup>
    {
        public DistributionGroupConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<DistributionGroup> builder)
        {
            builder
                .ToTable("DistributionGroup");

            builder
                .HasKey(dg => dg.Id);

            builder
                .Ignore(dg => dg.DomainEvents);

            builder
                .Property(dg => dg.GroupName)
                .IsRequired();

            builder
                .Property(dg => dg.GroupDescription)
                .IsRequired();

            builder
                .Property(dg => dg.GroupMapping)
                .IsRequired();

            builder
                .Property(dg => dg.EffDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
