using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Code).IsUnique();

            builder.Property(e => e.Code)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Language)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.UrlSlug)
                .HasMaxLength(30);

        }
    }
}