using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain.Events;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AccessTokenResult>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? TwoFactorCode { get; set; }          // see identity API
        public string? TwoFactorRecoveryCode { get; set; }  // see identity API
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AccessTokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenManager _tokenManager;
        private readonly IDomainEventService _domainEventService;

        public AuthenticateUserCommandHandler(IIdentityService identityService, ITokenManager tokenManager, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _tokenManager = tokenManager;
            _domainEventService = domainEventService;
        }

        public async Task<AccessTokenResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AuthenticateAsync(request.Email, request.Password);
            if (result.Succeeded)
            {
                var user = await _identityService.FindByEmail(request.Email);

                //var userClaims = await GetUserClaims(user);
                var claims = _identityService.GetClaims(user);

                //now with these claims, generate auth token pair
                var tokenPair = await _tokenManager.GenerateAuthTokensAsync(user, "dummy device id");

                await _domainEventService.Publish(new UserAuthenticatedEvent<Result>(user, result));
                return new AccessTokenResult(true, result.Errors)
                {
                    AccessToken = tokenPair.AccessToken,
                    RefreshToken = tokenPair.RefreshToken,
                    ExpiresIn = (tokenPair.RefreshTokenExpiration - DateTime.UtcNow).Minutes,
                };
            }

            await _domainEventService.Publish(new AuthenticationFailedEvent<Result>(request.Email, result));
            return new AccessTokenResult(false, result.Errors);
        }

    }
}
