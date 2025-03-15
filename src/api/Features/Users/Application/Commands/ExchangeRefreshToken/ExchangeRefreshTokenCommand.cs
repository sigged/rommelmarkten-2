using MediatR;
using Microsoft.Extensions.Logging;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ExchangeRefreshToken
{
    public enum ExchangeRefreshTokenError
    {
        None = 0,
        InvalidAccessToken = 1,
        InvalidOrExpiredRefreshToken = 2,
        InvalidSubject = 3,
    }

    public class ExchangeRefreshTokenResult : ResultBase<ExchangeRefreshTokenResult, ExchangeRefreshTokenError>
    {
        public ExchangeRefreshTokenResult() : base()
        {
            NewTokenPair = default!;
        }
        public ExchangeRefreshTokenResult(bool succeeded, AuthenticationTokenPair newTokenPair, IEnumerable<ExchangeRefreshTokenError> errors)
        {
            NewTokenPair = newTokenPair;
        }

        public AuthenticationTokenPair NewTokenPair { get; set; }

    }

    public class ExchangeRefreshTokenCommand : IRequest<ExchangeRefreshTokenResult>
    {
        public required AuthenticationTokenPair OldTokenPair { get; set; }

        public required string DeviceHash { get; set; }
    }

    public class ExchangeRefreshTokenCommandHandler : IRequestHandler<ExchangeRefreshTokenCommand, ExchangeRefreshTokenResult>
    {
        private readonly ILogger logger;
        private readonly IIdentityService identityService;
        private readonly ITokenManager tokenManager;

        public ExchangeRefreshTokenCommandHandler(ILogger<ExchangeRefreshTokenCommand> logger, IIdentityService identityService, ITokenManager tokenManager)
        {
            this.logger = logger;
            this.identityService = identityService;
            this.tokenManager = tokenManager;
        }

        public async Task<ExchangeRefreshTokenResult> Handle(ExchangeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            List<ExchangeRefreshTokenError> errors = [];

            var principal = await tokenManager.GetAccessTokenPrincipal(request.OldTokenPair.AccessToken, true);
            if (principal == null)
            {
                //invalid access token, might be forged/never issued.
                logger.LogInformation($"User {principal?.Identity?.Name} provided an invalid access token.");
                return ExchangeRefreshTokenResult.Failure([ExchangeRefreshTokenError.InvalidAccessToken]);
            }
            else
            {
                if (await tokenManager.IsValidRefreshToken(request.OldTokenPair.RefreshToken, request.DeviceHash))
                {
                    //valid access token and refreshtoken
                    try
                    {
                        // get user from token subject
                        var user = await identityService.FindByName(principal.Identity?.Name ?? string.Empty);

                        // revoke old refresh token
                        await tokenManager.RevokeToken(request.OldTokenPair.RefreshToken);

                        var tokenPair = await tokenManager.GenerateAuthTokensAsync(user, request.DeviceHash);
                        var result = ExchangeRefreshTokenResult.Success();
                        result.NewTokenPair = tokenPair;
                        return result;
                    }
                    catch(NotFoundException)
                    {
                        logger.LogInformation($"User {principal.Identity?.Name} not found for token subject.");
                        return ExchangeRefreshTokenResult.Failure([ExchangeRefreshTokenError.InvalidSubject]);
                    }
                }
                else
                {
                    //invalid refresh token.
                    logger.LogInformation($"User {principal.Identity?.Name} provided an invalid refresh token.");
                    return ExchangeRefreshTokenResult.Failure([ExchangeRefreshTokenError.InvalidOrExpiredRefreshToken]);
                }
            }
        }
    }
}
