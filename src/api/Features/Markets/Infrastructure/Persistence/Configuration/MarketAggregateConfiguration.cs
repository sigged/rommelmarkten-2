using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{

    public class MarketAggregateConfiguration : IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .HasMaxLength(100);

            builder.Property(e => e.Description)
                   .IsRequired(false);

            builder.HasOne(rm => rm.Province)
                   .WithMany(p => p.Markets)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(rm => rm.Invoices)
                   .WithOne(e => e.Market)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Revisions)
                   .WithOne(e => e.RevisedMarket)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(rm => rm.Themes)
                   .WithMany(th => th.Markets)
                   .UsingEntity<MarketWithTheme>();


            var pricingBuilder = builder.OwnsOne(e => e.Pricing);
            OwnedEntityConfigurationHelper.ConfigurePricing(pricingBuilder);

            var locationBuilder = builder.OwnsOne(e => e.Location);
            OwnedEntityConfigurationHelper.ConfigureLocation(locationBuilder);

            var imageBuilder = builder.OwnsOne(e => e.Image);
            OwnedEntityConfigurationHelper.ConfigureImage(imageBuilder);

            var organizerBuilder = builder.OwnsOne(e => e.Organizer);
            OwnedEntityConfigurationHelper.ConfigureOrganizer(organizerBuilder);
        }
    }
}