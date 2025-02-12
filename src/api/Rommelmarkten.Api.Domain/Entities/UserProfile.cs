using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Domain.Entities
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public bool Consented { get; set; }
        public Blob Avatar { get; set; }

    }
}
