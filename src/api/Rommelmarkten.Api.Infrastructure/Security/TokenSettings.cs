using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Security
{
    public class TokenSettings
    {
        public required string ApiJwtKey { get; set; }
        public required string JwtIssuer { get; set; }
        public required string JwtAudience { get; set; }
        public int JwtExpiryMinutes { get; set; }
        public int RefreshTokenExpiryMinutes { get; set; }
    }
}
