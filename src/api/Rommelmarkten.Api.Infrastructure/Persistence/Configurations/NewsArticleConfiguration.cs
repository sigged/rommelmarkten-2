﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class NewsArticleConfiguration : IEntityTypeConfiguration<NewsArticle>
    {
        public void Configure(EntityTypeBuilder<NewsArticle> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Content)
                .IsRequired();

        }
    }
}