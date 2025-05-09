﻿using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {

        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
