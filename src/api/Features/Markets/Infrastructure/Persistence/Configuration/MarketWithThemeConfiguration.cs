﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{
    public class MarketWithThemeConfiguration : IEntityTypeConfiguration<MarketWithTheme>
    {
        public void Configure(EntityTypeBuilder<MarketWithTheme> builder)
        {
            builder.HasKey(e => new { e.MarketId, e.ThemeId });

            builder.HasOne(e => e.Market)
                   .WithMany()
                   .HasForeignKey(e => e.MarketId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Theme)
                   .WithMany()
                   .HasForeignKey(e => e.ThemeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}