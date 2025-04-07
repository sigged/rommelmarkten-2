using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Domain
{
    public class ShoppingList : AuditableEntity<int>, IHasDomainEvent
    {
        public string Title { get; set; } = string.Empty;

        public string? Color { get; set; }

        public IList<ListAssociate> Associates { get; private set; } = new List<ListAssociate>();

        public IList<ListItem> Items { get; private set; } = new List<ListItem>();

        public ICollection<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
