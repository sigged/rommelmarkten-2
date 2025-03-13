using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
