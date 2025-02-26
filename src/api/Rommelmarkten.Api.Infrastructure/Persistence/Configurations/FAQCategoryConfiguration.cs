using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class FAQCategoryConfiguration : IEntityTypeConfiguration<FAQCategory>
    {
        public void Configure(EntityTypeBuilder<FAQCategory> builder)
        {

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