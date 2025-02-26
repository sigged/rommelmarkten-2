using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class FAQItemConfiguration : IEntityTypeConfiguration<FAQItem>
    {
        public void Configure(EntityTypeBuilder<FAQItem> builder)
        {
            builder.Property(e => e.Question)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Answer)
                .IsRequired();
        }
    }
}