using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Gateways
{
    public interface IUsersDbContext : IApplicationDbContextBase
    {

        DbSet<UserProfile> UserProfiles { get; set; }
    }
}
