using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ListItem> ListItems { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<ShoppingList> ShoppingLists { get; set; }

        DbSet<ListAssociate> ListAssociates { get; set; }

        DbSet<UserProfile> UserProfiles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken());
    }
}
