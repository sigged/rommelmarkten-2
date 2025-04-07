using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence.Configuration
{
    public class FAQItemConfiguration : IEntityTypeConfiguration<FAQItem>
    {
        public void Configure(EntityTypeBuilder<FAQItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Question)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Answer)
                .IsRequired();
        }
    }
}