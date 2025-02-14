using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Domain.Users
{
    public class UserProfile
    {
        public required string UserId { get; set; }
        public bool Consented { get; set; }
        public required Blob Avatar { get; set; }

    }
}
