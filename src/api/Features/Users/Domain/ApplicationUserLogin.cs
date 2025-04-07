using Microsoft.AspNetCore.Identity;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
