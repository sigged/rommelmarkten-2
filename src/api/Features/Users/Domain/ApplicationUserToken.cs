using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
