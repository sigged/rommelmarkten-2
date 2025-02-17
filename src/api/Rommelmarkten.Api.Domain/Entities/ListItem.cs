using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Entities
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
