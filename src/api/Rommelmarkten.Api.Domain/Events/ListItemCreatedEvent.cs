using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Domain.Events
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
