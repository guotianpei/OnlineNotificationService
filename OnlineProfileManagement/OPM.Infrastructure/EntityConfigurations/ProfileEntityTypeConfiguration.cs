using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<EntityProfile>
    {
        public ProfileEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<EntityProfile> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
