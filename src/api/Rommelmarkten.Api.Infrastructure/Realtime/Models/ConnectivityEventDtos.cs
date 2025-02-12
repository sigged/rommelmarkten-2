using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;

namespace Rommelmarkten.Api.Infrastructure.Realtime.Models
{
    public record UserJoinedGroupDto : EventBaseDto
    {
        public UserJoinedGroupDto(ShoppingListDto shoppingList, UserDto user) : base()
        {
            ShoppingList = shoppingList;
            User = user;
        }
        public ShoppingListDto ShoppingList { get; }
        public UserDto User { get; }
    }

    public record UserLeftGroupDto : EventBaseDto
    {
        public UserLeftGroupDto(ShoppingListDto shoppingList, UserDto user) : base()
        {
            ShoppingList = shoppingList;
            User = user;
        }
        public ShoppingListDto ShoppingList { get; }
        public UserDto User { get; }
    }
}
