using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{

    //insecure, so only admins can do this
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class GenerateEmailConfirmationTokenCommand : IRequest<TokenResult>
    {
        public required string UserId { get; set; }
    }

    public class GenerateEmailConfirmationTokenCommandHandler : IRequestHandler<GenerateEmailConfirmationTokenCommand, TokenResult>
    {
        private readonly IIdentityService identityService;
        private readonly IDomainEventService domainEventService;
        private readonly IResourceAuthorizationService resourceAuthorizationService;

        public GenerateEmailConfirmationTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService, IResourceAuthorizationService resourceAuthorizationService)
        {
            this.identityService = identityService;
            this.domainEventService = domainEventService;
            this.resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<TokenResult> Handle(GenerateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await identityService.GenerateEmailConfirmationTokenAsync(request.UserId);
            return new TokenResult(true, [])
            {
                Token = result
            };
        }
    }

}
