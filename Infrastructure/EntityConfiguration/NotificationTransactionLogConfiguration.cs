using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONP.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONP.Infrastructure
{
    public class NotificationTransactionLogConfiguration : IEntityTypeConfiguration<NotificationTransactionLog>
    {
        public NotificationTransactionLogConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<NotificationTransactionLog> builder)
        {
            builder
                .ToTable("NotificationTransactionLog");

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
                .Property(nh => nh.TransactionDateTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
