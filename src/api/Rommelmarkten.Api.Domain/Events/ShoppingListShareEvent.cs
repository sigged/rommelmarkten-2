using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Domain.Events
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
