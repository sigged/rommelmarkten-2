using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pipelines.Sockets.Unofficial.Buffers;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class OwnedEntityConfigurationHelper
    {
        public static OwnedNavigationBuilder<TOwner, MarketLocation> ConfigureLocation<TOwner>(OwnedNavigationBuilder<TOwner, MarketLocation> locationBuilder)
            where TOwner : class
        {

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
            return locationBuilder;
        }

        public static OwnedNavigationBuilder<TOwner, MarketImage> ConfigureImage<TOwner>(OwnedNavigationBuilder<TOwner, MarketImage> imageBuilder)
            where TOwner : class
        {
            imageBuilder.Property(e => e.ImageUrl)
                          .HasMaxLength(255)
                          .IsRequired();

            imageBuilder.Property(e => e.ImageThumbUrl)
                           .HasMaxLength(255)
                           .IsRequired();

            return imageBuilder;
        }

        public static OwnedNavigationBuilder<TOwner, Organizer> ConfigureOrganizer<TOwner>(OwnedNavigationBuilder<TOwner, Organizer> organizerBuilder)
            where TOwner : class
        {

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

            return organizerBuilder;
        }

        public static OwnedNavigationBuilder<TOwner, MarketPricing> ConfigurePricing<TOwner>(OwnedNavigationBuilder<TOwner, MarketPricing> pricingBuilder)
            where TOwner : class
        {
            pricingBuilder.Property(e => e.EntryPrice)
                         .HasPrecision(18, 2);
            pricingBuilder.Property(e => e.StandPrice)
                          .HasPrecision(18, 2);

            return pricingBuilder;
        }

        //public static void ConfigureAvatar(OwnedNavigationBuilder<UserProfile, Blob> avatarBuilder)
        //{
        //    avatarBuilder.Property(e => e.Type)
        //       .IsRequired();

        //    avatarBuilder.Property(e => e.Content)
        //        .IsRequired();

        //    avatarBuilder.Property(e => e.ContentHash)
        //        .IsRequired();

        //    avatarBuilder.Property(e => e.Name)
        //        .IsRequired();
        //}
    }
}