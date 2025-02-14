using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.OwnsOne(e => e.Avatar)
                .Property(e => e.Type)
                .IsRequired();

            builder.OwnsOne(e => e.Avatar)
                .Property(e => e.Content)
                .IsRequired();

            builder.OwnsOne(e => e.Avatar)
                .Property(e => e.ContentHash)
                .IsRequired();

            builder.OwnsOne(e => e.Avatar)
                .Property(e => e.Name)
                .IsRequired();
        }
    }
}