﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineNotificationLogEF
{
   


    public class OnlineNotificationLogContext : DbContext
    {       
        public OnlineNotificationLogContext(DbContextOptions<OnlineNotificationLogContext> options) : base(options)
        {
        }

        public DbSet<NotificationLogEntry> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {          
            builder.Entity<NotificationLogEntry>(ConfigureNotificationEventLogEntry);
        }

        void ConfigureNotificationEventLogEntry(EntityTypeBuilder<NotificationLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.CreationTime)
                .IsRequired();

            builder.Property(e => e.State)
                .IsRequired();

            builder.Property(e => e.TimesSent)
                .IsRequired();

            builder.Property(e => e.EventTypeName)
                .IsRequired();

        }
    }
}
