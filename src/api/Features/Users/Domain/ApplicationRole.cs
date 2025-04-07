using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationRole : IdentityRole, IRole
    {
        public ApplicationRole()
        {
            
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            
        }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = [];
    }
}
