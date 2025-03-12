using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
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

        }
    }
}