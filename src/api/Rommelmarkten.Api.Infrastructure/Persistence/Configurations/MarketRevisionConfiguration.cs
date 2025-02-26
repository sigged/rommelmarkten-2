using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketRevisionConfiguration : IEntityTypeConfiguration<MarketRevision>
    {
        public void Configure(EntityTypeBuilder<MarketRevision> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .HasMaxLength(100);

            builder.Property(e => e.Description)
                   .IsRequired(false);

            builder.HasMany(rm => rm.Themes)
                .WithMany(th => th.MarketRevisions)
                .UsingEntity<MarketRevisionWithTheme>();

            var locationBuilder = builder.OwnsOne(e => e.Location);
            var imageBuilder = builder.OwnsOne(e => e.Image);
            var organizerBuilder = builder.OwnsOne(e => e.Organizer);

            OwnedEntityConfigurationHelper.ConfigureLocation(locationBuilder);
            OwnedEntityConfigurationHelper.ConfigureImage(imageBuilder);
            OwnedEntityConfigurationHelper.ConfigureOrganizer(organizerBuilder);
        }
    }
}