using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Identity;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence
{
    public class UsersDbContext : ApplicationDbContextBase, IUsersDbContext
    {
        public UsersDbContext(
            DbContextOptions<UsersDbContext> options,
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

        public required DbSet<UserProfile> UserProfiles { get; set; }


    }
}
