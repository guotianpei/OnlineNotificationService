using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationProcessor.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.Infrastructure.EntityConfiguration
{
    public class EntityProfileConfiguration : IEntityTypeConfiguration<EntityProfile>
    {
        public EntityProfileConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<EntityProfile> builder)
        {
            builder
                .ToTable("EntityProfile");

            builder
                .HasKey(nr => nr.ID);

            //builder
            //    .Ignore(nr => nr.DomainEvents);

            //builder
            //    .HasOne(nr => nr.EntityProfile)
            //    .WithMany()
            //    .HasForeignKey(nr => nr.EntityID);

            builder
                .Property(nr => nr.EntityID)
                .IsRequired();

        }
    }
}
