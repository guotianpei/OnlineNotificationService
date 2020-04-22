using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;

namespace OPM.Infrastructure.EntityConfigurations 
{
    public class ProfileComChannelEntityTypeConfiguration : IEntityTypeConfiguration<ProfileComChannel>
    {
        public ProfileComChannelEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ProfileComChannel> builder)
        {
            throw new NotImplementedException();
        }
    }
}
