using ONP.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONP.Infrastructure
{
    public class IntegrationEventLogConfiguration : IEntityTypeConfiguration<IntegrationEventLogEntry>
    {
        public IntegrationEventLogConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                   .IsRequired();

            builder.Property(e => e.Content)
                   .IsRequired();

            builder
                .Ignore(ep => ep.EventTypeShortName);

            builder
                .Ignore(ep => ep.IntegrationEvent);

            builder
                .Ignore(ep => ep.EventState);

            builder.Property(e => e.CreationTime)
                   .IsRequired();

            builder.Property(ep => ep.CreationTime)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.State)
                   .IsRequired();

            builder.Property(e => e.TimesSent)
                   .IsRequired();

            builder.Property(e => e.EventTypeName)
                   .IsRequired();
        }
    }
}
