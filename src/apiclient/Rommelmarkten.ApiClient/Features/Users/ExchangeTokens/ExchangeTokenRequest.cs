using Rommelmarkten.ApiClient.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.ApiClient.Features.Users.ExchangeTokens
{
    public class ExchangeTokenRequest
    {
        public required TokenPair OldTokenPair { get; set; }

        public required string DeviceHash { get; set; }
    }
}
