using Rommelmarkten.Api.Common.Domain;
using System.Security.Principal;

namespace Rommelmarkten.Api.Common.Application.Security
{
    public interface ITokenManager
    {
        string GenerateRandomToken(int size = 32);

        Task<AuthenticationTokenPair> GenerateAuthTokensAsync(IUser user, string deviceHash);
        Task RevokeToken(string refreshToken);
        Task<bool> IsValidRefreshToken(string refreshToken, string deviceHash);
        Task<IPrincipal> GetAccessTokenPrincipal(string accessToken, bool ignoreExpiration = false);

        //Task<bool> IsValidRefreshToken(string refreshToken);

        //Task RevokeToken(string refreshToken);

        //Task<AuthenticationTokenPair> GenerateAuthTokens(IUser user);

        ///// <summary>
        ///// Returns the principal from a token, returns null if the token is invalid or expired
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="ignoreLifetime"></param>
        ///// <returns></returns>
        //Task<IPrincipal> GetAccessTokenPrincipal(string accessToken, bool ignoreExpiration = false);
    }

}
