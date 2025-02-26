using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                   .IsRequired(false);

            builder.HasOne(rm => rm.Province)
                   .WithMany(p => p.Markets)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(rm => rm.Themes)
                .WithMany(th => th.Markets)
                .UsingEntity<MarketWithTheme>();

            builder.OwnsOne(e => e.Organizer);
            builder.OwnsOne(e => e.Image);
            builder.OwnsOne(e => e.Location);

            //builder.OwnsOne(e => e.Revision)
            //    .HasOne(e => e.RevisedMarket)
            //    .WithOne();
            //.Property(e => e.EntryPrice).HasPrecision(18, 2);

            var pricingBuilder = builder.OwnsOne(e => e.Pricing);

            pricingBuilder.Property(e => e.EntryPrice)
                          .HasPrecision(18, 2);
            pricingBuilder.Property(e => e.StandPrice)
                          .HasPrecision(18, 2);


            var locationBuilder = builder.OwnsOne(e => e.Location);

            locationBuilder.Property(e => e.Hall)
                           .HasMaxLength(100);

            locationBuilder.Property(e => e.Address)
                           .HasMaxLength(100)
                           .IsRequired();

            locationBuilder.Property(e => e.PostalCode)
                           .HasMaxLength(10)
                           .IsRequired();

            locationBuilder.Property(e => e.City)
                           .HasMaxLength(100)
                           .IsRequired();

            locationBuilder.Property(e => e.Country)
                           .HasMaxLength(100)
                           .IsRequired();

            locationBuilder.Property(e => e.CoordLatitude)
                           .HasMaxLength(100);

            locationBuilder.Property(e => e.CoordLatitude)
                           .HasMaxLength(100);


            var imageBuilder = builder.OwnsOne(e => e.Image);

            imageBuilder.Property(e => e.ImageUrl)
                           .HasMaxLength(255)
                           .IsRequired();

            imageBuilder.Property(e => e.ImageThumbUrl)
                           .HasMaxLength(255)
                           .IsRequired();


            var organizerBuilder = builder.OwnsOne(e => e.Organizer);

            organizerBuilder.Property(e => e.Name)
                           .HasMaxLength(100)
                           .IsRequired();

            organizerBuilder.Property(e => e.Phone)
                           .HasMaxLength(100)
                           .IsRequired();

            organizerBuilder.Property(e => e.Email)
                           .HasMaxLength(100)
                           .IsRequired();

            organizerBuilder.Property(e => e.URL)
                           .HasMaxLength(100);

            organizerBuilder.Property(e => e.ContactNotes)
                           .HasMaxLength(200);

        }
    }
}