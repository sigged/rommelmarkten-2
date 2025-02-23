using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class BannerTypeConfiguration : IEntityTypeConfiguration<BannerType>
    {
        public void Configure(EntityTypeBuilder<BannerType> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasPrecision(18, 2);
        }
    }
}