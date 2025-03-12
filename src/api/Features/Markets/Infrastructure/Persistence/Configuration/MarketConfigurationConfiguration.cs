using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{
    public class MarketConfigurationConfiguration : IEntityTypeConfiguration<MarketConfiguration>
    {
        public void Configure(EntityTypeBuilder<MarketConfiguration> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasPrecision(18, 2);
        }
    }
}