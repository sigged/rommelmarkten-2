using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Entities
{
    public class ListItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public ShoppingList List { get; set; }

        public int ListId { get; set; }

        public Category Category { get; set; }

        public int? CategoryId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
