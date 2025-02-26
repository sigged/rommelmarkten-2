using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{



    public class MarketAggregateConfiguration : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.Property(e => e.Title)
                   .HasMaxLength(100);

            builder.Property(e => e.Description)
                   .HasMaxLength(4000);

            builder.HasOne(rm => rm.Province)
                   .WithMany(p => p.Markets)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(rm => rm.Themes)
                .WithMany(th => th.Markets)
                .UsingEntity<MarketWithTheme>();

            builder.OwnsOne(e => e.Organizer);
            builder.OwnsOne(e => e.Image);
            builder.OwnsOne(e => e.Location);
            
            builder.OwnsOne(e => e.Pricing)
                .Property(e => e.EntryPrice).HasPrecision(18, 2);
            builder.OwnsOne(e => e.Pricing)
                .Property(e => e.StandPrice).HasPrecision(18, 2);

        }
    }
}