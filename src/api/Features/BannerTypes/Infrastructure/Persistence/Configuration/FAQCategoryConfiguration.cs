﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence.Configuration
{
    public class FAQCategoryConfiguration : IEntityTypeConfiguration<FAQCategory>
    {
        public void Configure(EntityTypeBuilder<FAQCategory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(e => e.Order)
                .IsRequired();

            builder.HasMany(cat => cat.FAQItems)
                .WithOne(item => item.Category)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}