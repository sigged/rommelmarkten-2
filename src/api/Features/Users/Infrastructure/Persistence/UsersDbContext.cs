using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime) 
            : base(options)
        {
        }

        public required DbSet<UserProfile> UserProfiles { get; set; }

        public Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
