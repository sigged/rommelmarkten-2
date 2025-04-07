using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; } = default!;
    }
}
