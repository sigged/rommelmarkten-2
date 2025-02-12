using Rommelmarkten.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class ListAssociateConfiguration : IEntityTypeConfiguration<ListAssociate>
    {
        public void Configure(EntityTypeBuilder<ListAssociate> builder)
        {
            builder.HasKey(t => new { t.ListId, t.AssociateId });

            builder.HasOne(t => t.List)
                .WithMany(t => t.Associates)
                .HasForeignKey(e => e.ListId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}