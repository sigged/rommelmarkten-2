using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence
{
    public class ShoppingListsDbContext : ApplicationDbContext, IShoppingListsDbContext
    {
        public ShoppingListsDbContext(DbContextOptions options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime) 
            : base(options, currentUserService, domainEventService, dateTime)
        {
        }

        public required DbSet<ListItem> ListItems { get; set; }

        public required DbSet<Category> Categories { get; set; }

        public required DbSet<ShoppingList> ShoppingLists { get; set; }

        public required DbSet<ListAssociate> ListAssociates { get; set; }

    }
}
