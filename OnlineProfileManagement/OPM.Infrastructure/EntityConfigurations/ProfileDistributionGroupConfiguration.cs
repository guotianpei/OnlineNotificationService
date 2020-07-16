using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class ProfileDistributionGroupConfiguration : IEntityTypeConfiguration<ProfileDistributionGroup>
    {
        public ProfileDistributionGroupConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<ProfileDistributionGroup> builder)
        {
            builder
                .ToTable("ProfileDistributionGroup");

            builder
                .HasKey(pdg => pdg.Id);

            builder
                .Ignore(pdg => pdg.DomainEvents);

            builder
                .HasOne(pdg => pdg.DistributionGroup)
                .WithMany()
                .HasForeignKey(pdg => pdg.GroupID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(pdg => pdg.EffDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
