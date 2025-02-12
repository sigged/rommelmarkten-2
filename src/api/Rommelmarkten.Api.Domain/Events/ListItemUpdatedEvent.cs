using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Domain.Events
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
