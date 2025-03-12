using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Features.ShoppingLists.Events
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
