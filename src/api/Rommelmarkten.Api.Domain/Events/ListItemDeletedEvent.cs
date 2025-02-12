using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Domain.Events
{
    public class ListItemRemovedEvent : DomainEvent
    {
        public ListItemRemovedEvent(ListItem item)
        {
            Item = item;
        }

        public ListItem Item { get; }
    }
}
