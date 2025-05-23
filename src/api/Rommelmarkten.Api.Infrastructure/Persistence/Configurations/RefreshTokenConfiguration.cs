﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Infrastructure.Identity;

namespace Rommelmarkten.Infrastructure.Data.Identity
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DeviceHash)
                .HasMaxLength(100);

            builder.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(250);

            // RefreshToken <--> ApplicationUser
            builder
                .HasOne(rt => rt.User)
                .WithMany(appuser => appuser.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
