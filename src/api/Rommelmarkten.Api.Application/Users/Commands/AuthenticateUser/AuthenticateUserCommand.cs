using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.Users.Models;
using Rommelmarkten.Api.Domain.Events;

namespace Rommelmarkten.Api.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AccessTokenResult>
    {
        public required string UserName { get; set; }
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
            var result = await _identityService.AuthenticateAsync(request.UserName, request.Password);
            if (result.Succeeded)
            {
                var entity = await _identityService.FindByName(request.UserName);
                await _domainEventService.Publish(new UserAuthenticatedEvent<Result>(entity, result));
                return new AccessTokenResult(true, result.Errors)
                {
                    AccessToken = await _tokenManager.GenerateAuthTokenAsync(entity)
                };
            }

            await _domainEventService.Publish(new AuthenticationFailedEvent<Result>(request.UserName, result));
            return new AccessTokenResult(false, result.Errors);
        }
    }
}
