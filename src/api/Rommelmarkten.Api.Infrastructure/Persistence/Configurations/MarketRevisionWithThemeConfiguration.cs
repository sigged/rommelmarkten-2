using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketRevisionWithThemeConfiguration : IEntityTypeConfiguration<MarketRevisionWithTheme>
    {
        public void Configure(EntityTypeBuilder<MarketRevisionWithTheme> builder)
        {
            builder.HasKey(e => new { e.MarketRevisionId, e.ThemeId });

            builder.HasOne(e => e.MarketRevision)
                   .WithMany()
                   .HasForeignKey(e => e.MarketRevisionId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Theme)
                   .WithMany()
                   .HasForeignKey(e => e.ThemeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}