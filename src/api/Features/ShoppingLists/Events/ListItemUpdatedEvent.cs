using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Events
{
    public class ListItemUpdatedEvent : DomainEvent
    {
        public ListItemUpdatedEvent(ListItem item)
        {
            Item = item;
        }

        public ListItem Item { get; }
    }
}
