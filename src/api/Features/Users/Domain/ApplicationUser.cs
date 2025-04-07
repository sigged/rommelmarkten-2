using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationUser : IdentityUser, IUser
    {

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; } = [];
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; } = [];
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = [];
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
    }
}
