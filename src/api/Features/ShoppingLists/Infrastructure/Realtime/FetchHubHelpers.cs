namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime
{
    public static class FetchHubHelpers
    {
        public const string ShoppingListChannelPrefix = "shoppinglist/";
        public static string GetShoppingListGroupName(string listId)
        {
            return ShoppingListChannelPrefix + listId;
        }

    }

}
