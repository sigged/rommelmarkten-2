using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            
        }
    }
}
