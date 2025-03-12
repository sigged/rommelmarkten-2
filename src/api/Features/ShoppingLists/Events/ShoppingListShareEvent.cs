using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Features.ShoppingLists.Events
{
    public class ShoppingListShareEvent : DomainEvent
    {
        public ShoppingListShareEvent(ShoppingList shoppingList, IUser associate, bool shared)
        {
            List = shoppingList;
            Associate = associate;
            Shared = shared;
        }

        public ShoppingList List { get; }

        public IUser Associate { get; }

        public bool Shared { get; }

    }
}
