using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence
{
    public class ShoppingListsDbContext : ApplicationDbContextBase, IShoppingListsDbContext
    {
        public ShoppingListsDbContext(
            DbContextOptions<ShoppingListsDbContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime
            )
                : base(options, currentUserService, domainEventService, dateTime)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public required DbSet<ListItem> ListItems { get; set; }

        public required DbSet<Category> Categories { get; set; }

        public required DbSet<ShoppingList> ShoppingLists { get; set; }

        public required DbSet<ListAssociate> ListAssociates { get; set; }

    }

}
