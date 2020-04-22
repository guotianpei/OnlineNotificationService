using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM.Domain.Aggregates.ProfileAggregate;

namespace OPM.Infrastructure.EntityConfigurations
{
    public class EntityProfileConfiguration :IEntityTypeConfiguration<EntityProfile>
    {
        public EntityProfileConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<EntityProfile> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
