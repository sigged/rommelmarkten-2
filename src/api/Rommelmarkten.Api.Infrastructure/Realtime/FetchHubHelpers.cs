using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;
using Rommelmarkten.Api.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Rommelmarkten.Api.Infrastructure.Realtime
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
