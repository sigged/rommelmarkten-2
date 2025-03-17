using MediatR;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries.GenerateEmailConfirmationToken
{

    //insecure, so only admins can do this
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class GenerateEmailConfirmationTokenCommand : IRequest<TokenResult>
    {
        public required string UserId { get; set; }
    }

    public class GenerateEmailConfirmationTokenCommandHandler : IRequestHandler<GenerateEmailConfirmationTokenCommand, TokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;
        private readonly IResourceAuthorizationService resourceAuthorizationService;

        public GenerateEmailConfirmationTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService, IResourceAuthorizationService resourceAuthorizationService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
            this.resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<TokenResult> Handle(GenerateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            //if (!await resourceAuthorizationService.AuthorizeAny(null, CorePolicies.MustBeAdmin))
            //{
            //    throw new ForbiddenAccessException();
            //}

            var result = await _identityService.GenerateEmailConfirmationTokenAsync(request.UserId);
            return new TokenResult(true, [])
            {
                Token = result
            };
        }
    }

}
