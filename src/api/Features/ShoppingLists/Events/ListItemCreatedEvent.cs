using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Events
{
    public class ListItemCreatedEvent : DomainEvent
    {
        public ListItemCreatedEvent(ListItem item)
        {
            Item = item;
        }

        public ListItem Item { get; }
    }
}
