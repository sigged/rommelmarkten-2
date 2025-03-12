using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models
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
