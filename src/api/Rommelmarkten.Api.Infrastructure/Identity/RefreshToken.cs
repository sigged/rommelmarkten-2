using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Infrastructure.Identity
{
    public class RefreshToken : EntityBase
    {

        public RefreshToken(string token, DateTime expires, string userId, string deviceHash)
        {
            Id = Guid.NewGuid();
            Token = token;
            Expires = expires;
            UserId = userId;
            DeviceHash = deviceHash;
        }

        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string DeviceHash { get; set; }
    }
}
