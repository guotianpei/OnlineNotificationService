﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class ProfileResourceConfiguration : IEntityTypeConfiguration<ProfileResource>
    {
        public ProfileResourceConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<ProfileResource> builder)
        {
            builder
                .ToTable("ProfileResource");

            builder
                .HasKey(pr => pr.Id);

            builder
                .Ignore(pr => pr.DomainEvents);

            builder
                .Property(pr => pr.ResourceName)
                .IsRequired();

            builder
                .Property(pr => pr.EffDate)
                .HasDefaultValueSql("GETDATE()");

            //builder
            //    .HasData
            //    (
            //     new ProfileResource
            //     {
            //         Id = 1,
            //         ResourceName = "TPA"
            //     },
            //     new ProfileResource
            //     {
            //         Id = 2,
            //         ResourceName = "PEA"
            //     }
            //    );
        }
    }
}
