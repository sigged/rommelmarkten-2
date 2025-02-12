using Rommelmarkten.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {

        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
    }
}
