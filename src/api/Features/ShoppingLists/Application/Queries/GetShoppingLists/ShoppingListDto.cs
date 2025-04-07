using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists
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
