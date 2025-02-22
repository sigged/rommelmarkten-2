using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketConfigurationConfiguration : IEntityTypeConfiguration<MarketConfiguration>
    {
        public void Configure(EntityTypeBuilder<MarketConfiguration> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(4000)
                .IsRequired();

        }
    }
}