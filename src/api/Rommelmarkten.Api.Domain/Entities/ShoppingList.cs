using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Entities
{
    public class ShoppingList : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Color { get; set; }

        public IList<ListAssociate> Associates { get; private set; } = new List<ListAssociate>();

        public IList<ListItem> Items { get; private set; } = new List<ListItem>();

        public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>(); 
    }
}
