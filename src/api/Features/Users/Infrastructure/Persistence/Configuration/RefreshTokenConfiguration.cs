using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Persistence.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DeviceHash)
                .HasMaxLength(300);

            builder.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(300);

            builder.Ignore(e => e.TokenRaw);

            // RefreshToken <--> ApplicationUser
            builder
                .HasOne(rt => rt.User)
                .WithMany(appuser => appuser.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
