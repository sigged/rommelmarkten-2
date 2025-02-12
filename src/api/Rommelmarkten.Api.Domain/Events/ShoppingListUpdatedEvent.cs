using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Domain.Events
{
    public class ShoppingListUpdatedEvent : DomainEvent
    {
        public ShoppingListUpdatedEvent(ShoppingList shoppingList)
        {
            List = shoppingList;
        }

        public ShoppingList List { get; }

    }
}
