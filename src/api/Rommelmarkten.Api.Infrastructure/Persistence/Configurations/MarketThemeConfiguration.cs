using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketThemeConfiguration : IEntityTypeConfiguration<MarketTheme>
    {
        public void Configure(EntityTypeBuilder<MarketTheme> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.ImageUrl)
                .HasMaxLength(2048);

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasPrecision(18,2);
        }
    }
}