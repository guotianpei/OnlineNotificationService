using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class ProfileComChannelConfiguration : IEntityTypeConfiguration<ProfileComChannel>
    {
        public ProfileComChannelConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<ProfileComChannel> builder)
        {
            builder
                .ToTable("ProfileComChannel");

            builder
                .HasKey(pcc => pcc.Id);

            builder
                .Ignore(pcc => pcc.DomainEvents);

            builder
                .Property(pcc => pcc.ComChannel)
                .IsRequired();

            builder
               .Property(pcc => pcc.Value)
               .IsRequired();

            builder
                .Property(pcc => pcc.Enabled)
                .IsRequired();

            builder
            .Property(e => e.Status)
            .HasConversion(
            v => v.ToString(),
            v => (ComChannelStatus)Enum.Parse(typeof(ComChannelStatus), v));

        }
    }
}
