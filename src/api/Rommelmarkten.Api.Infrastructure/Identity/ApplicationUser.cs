using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }
    }
}
