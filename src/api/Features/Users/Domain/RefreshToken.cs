﻿namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

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

        //holds a raw token, not persisted
        public string? TokenRaw { get; set; }
    }
}
