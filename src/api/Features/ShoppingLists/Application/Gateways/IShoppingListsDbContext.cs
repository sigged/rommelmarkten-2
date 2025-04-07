using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways
{
    public interface IShoppingListsDbContext : IApplicationDbContextBase
    {

        DbSet<ListItem> ListItems { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<ShoppingList> ShoppingLists { get; set; }

        DbSet<ListAssociate> ListAssociates { get; set; }

    }
}
