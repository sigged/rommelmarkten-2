using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence
{
    public class UsersDbContext : ApplicationDbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime) 
            : base(options, currentUserService, domainEventService, dateTime)
        {
        }

        public required DbSet<UserProfile> UserProfiles { get; set; }
    }
}
