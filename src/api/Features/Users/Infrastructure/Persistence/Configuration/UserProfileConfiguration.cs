using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.WebApi.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(e => e.OwnedBy);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Address)
                .HasMaxLength(100);

            builder.Property(e => e.PostalCode)
                           .HasMaxLength(10);

            builder.Property(e => e.City)
                           .HasMaxLength(100);

            builder.Property(e => e.Country)
                           .HasMaxLength(100);

            builder.Property(e => e.VAT)
                           .HasMaxLength(30);

            //var avatarBuilder = builder.OwnsOne(e => e.Avatar);

            //OwnedEntityConfigurationHelper.ConfigureAvatar(avatarBuilder);


        }
    }
}