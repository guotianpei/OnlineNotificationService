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
            builder
                .ToTable("EntityProfile");

            builder
                .HasKey(ep => ep.Id);

            builder
                .HasOne(ep=>ep.ProfileResource)
                .WithMany()
                .HasForeignKey(ep=>ep.ResourceID)
                .IsRequired();

            builder
                .Property(ep => ep.EntityID)
                .IsRequired();

            builder
                .Property(ep => ep.EffDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
