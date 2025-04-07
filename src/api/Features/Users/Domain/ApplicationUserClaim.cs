using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
