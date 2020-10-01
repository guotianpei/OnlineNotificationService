using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationProcessor.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationProcessor.API.Infrastructure.EntityConfiguration
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public NotificationTemplateConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder
                .ToTable("NotificationTemplate");

            builder
                .HasKey(nr => nr.ID);

            //builder
            //    .Ignore(nr => nr.DomainEvents);

            //builder
            //    .HasOne(nr => nr.EntityProfile)
            //    .WithMany()
            //    .HasForeignKey(nr => nr.EntityID);

            builder
               .Property(nr => nr.TopicName)
               .IsRequired();

            builder
                .Property(nr => nr.ComChannel)
                .IsRequired();

            builder
               .Property(nr => nr.From)
               .IsRequired();

            builder
             .Property(nr => nr.TemplateFile)
             .IsRequired();

            builder
             .Property(nr => nr.EffectiveDate)
             .IsRequired();

            builder
             .Property(nr => nr.From)
             .IsRequired();
            
            builder
            .Property(nr => nr.UpdateBy)
            .IsRequired();

        }
    }
}
