using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class NotificationHistoryConfiguration : IEntityTypeConfiguration<NotificationHistory>
    {
        public NotificationHistoryConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<NotificationHistory> builder)
        {
            builder
                .ToTable("NotificationLog");

            builder
                .HasKey(nh => nh.Id);

            builder
                .Ignore(nh => nh.DomainEvents);

            //builder
            //    .HasOne(nh => nh.EntityProfile)
            //    .WithMany()
            //    .HasForeignKey(nh => nh.EntityID);

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
