using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    public class FailureNotifyCodesConfiguration : IEntityTypeConfiguration<FailureNotifyCodes>
    {
        public FailureNotifyCodesConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<FailureNotifyCodes> builder)
        {
            builder
                .ToTable("FailureNotifyCodes");

            builder
                .HasKey(nr => nr.ID);

            //builder
            //    .Ignore(nr => nr.DomainEvents);

            //builder
            //    .HasOne(nr => nr.EntityProfile)
            //    .WithMany()
            //    .HasForeignKey(nr => nr.EntityID);


        }
    }
}
