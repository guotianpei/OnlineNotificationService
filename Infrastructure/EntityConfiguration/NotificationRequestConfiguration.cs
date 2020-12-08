using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    public class NotificationRequestConfiguration : IEntityTypeConfiguration<NotificationRequest>
    {
        public NotificationRequestConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<NotificationRequest> builder)
        {
            builder
                .ToTable("NotificationRequest");

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

            builder
                .Property(nr => nr.ComChannel)
                .IsRequired();

            builder
               .Property(nr => nr.TopicID)
               .IsRequired();

            builder
                .Property(nr => nr.ID)
                .HasDefaultValueSql("newid()");

            builder
                .Property(ep => ep.RequestDatetime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}