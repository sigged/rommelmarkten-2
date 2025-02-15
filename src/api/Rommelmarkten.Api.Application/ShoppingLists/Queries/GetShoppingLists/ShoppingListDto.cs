using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Entities;
using System;

namespace Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists
{
    public class ShoppingListDto : IMapFrom<ShoppingList>
    {
        public ShoppingListDto()
        {
        }

        public required int Id { get; set; }

        public required string Title { get; set; }

        public string? Color { get; set; }

        public bool IsShared { get; set; }

        public string? Owner { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

    }
}
