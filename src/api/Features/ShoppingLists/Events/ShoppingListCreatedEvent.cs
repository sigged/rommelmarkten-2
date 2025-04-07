using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Events
{
    public class ShoppingListCreatedEvent : DomainEvent
    {
        public ShoppingListCreatedEvent(ShoppingList shoppingList)
        {
            List = shoppingList;
        }

        public ShoppingList List { get; }

    }
}
