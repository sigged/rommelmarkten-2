using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Domain
{
    public class ListItem : AuditableEntity<int>, IHasDomainEvent
    {
        public ShoppingList? List { get; set; }

        public int ListId { get; set; }

        public Category? Category { get; set; }

        public int? CategoryId { get; set; }

        public required string Title { get; set; }

        public bool Done { get; set; }

        public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
