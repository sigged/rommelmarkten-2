﻿using Rommelmarkten.Api.Domain.Users;
using System.Security.Principal;

namespace Rommelmarkten.Api.Application.Common.Security
{
    public interface ITokenManager
    {
        string GenerateRandomToken(int size = 32);

        [Obsolete("Use GenerateAuthTokenAsync instead")]
        Task<string> GenerateAuthTokenAsync(IUser user);
        Task<AuthenticationTokenPair> GenerateAuthTokensAsync(IUser user, string deviceId);
        Task RevokeToken(string refreshToken);
        Task<bool> IsValidRefreshToken(string refreshToken);
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
