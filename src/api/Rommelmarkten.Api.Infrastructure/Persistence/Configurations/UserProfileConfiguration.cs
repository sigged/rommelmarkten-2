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

            var avatarBuilder = builder.OwnsOne(e => e.Avatar);

            OwnedEntityConfigurationHelper.ConfigureAvatar(avatarBuilder);

           
        }
    }
}