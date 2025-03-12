using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.WebApi.Persistence.Configurations
{
    public class AffiliateAdConfiguration : IEntityTypeConfiguration<AffiliateAd>
    {
        public void Configure(EntityTypeBuilder<AffiliateAd> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(t => t.AffiliateName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.ImageUrl)
                .HasMaxLength(2048)
                .IsRequired();

            builder.Property(t => t.AffiliateURL)
                .HasMaxLength(2048)
                .IsRequired();
        }
    }
}