using Rommelmarkten.ApiClient.Security;

namespace Rommelmarkten.ApiClient.Features.Users.ExchangeTokens
{
    public class ExchangeTokenRequest
    {
        public required TokenPair OldTokenPair { get; set; }

        public required string DeviceHash { get; set; }
    }
}
