using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Code).IsUnique();

            builder.Property(e => e.Code)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.Language)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}