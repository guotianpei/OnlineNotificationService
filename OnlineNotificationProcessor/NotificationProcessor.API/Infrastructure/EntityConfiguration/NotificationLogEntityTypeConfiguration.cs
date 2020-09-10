using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationProcessor.API.Model;

namespace NotificationProcessor.API.Infrastructure.EntityConfiguration
{
    public class NotificationLogEntityTypeConfiguration : IEntityTypeConfiguration<NotificationLog>
    {
        public NotificationLogEntityTypeConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<NotificationLog> builder)
        {
            builder
                .ToTable("NotificationLog");

            builder
                .HasKey(nh => nh.TrackingID);

            //builder
            //    .Ignore(nh => nh.DomainEvents);

            //builder
            //    .HasOne(nh => nh.EntityProfile)
            //    .WithMany()
            //    .HasForeignKey(nh => nh.EntityID);

            builder
                .Property(nh => nh.EntityID)
                .IsRequired();

            builder
                .Property(nh => nh.ComChannel)
                .IsRequired();

            builder
               .Property(nh => nh.Recipient)
               .IsRequired();

            builder
                .Property(nh => nh.MessageBody)
                .IsRequired();

            builder
                .Property(nh => nh.NotificationDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
