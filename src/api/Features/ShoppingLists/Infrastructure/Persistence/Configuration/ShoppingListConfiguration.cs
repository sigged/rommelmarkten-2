using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence.Configuration
{
    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.Ignore(t => t.DomainEvents);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Color)
                .HasMaxLength(10);
        }
    }
}