using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {
        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        [ProtectedPersonalData]
        public required new string UserName { get; set; }

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }
    }
}
