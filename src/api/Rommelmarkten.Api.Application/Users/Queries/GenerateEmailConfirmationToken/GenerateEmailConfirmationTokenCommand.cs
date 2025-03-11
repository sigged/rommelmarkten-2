using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.Users.Models;

namespace Rommelmarkten.Api.Application.Users.Queries.GenerateEmailConfirmationToken
{

    //insecure, so only admins can do this
    [Authorize(Policy = Policies.MustBeAdmin)]
    public class GenerateEmailConfirmationTokenCommand : IRequest<TokenResult>
    {
        public required string UserId { get; set; }
    }

    public class GenerateEmailConfirmationTokenCommandHandler : IRequestHandler<GenerateEmailConfirmationTokenCommand, TokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public GenerateEmailConfirmationTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<TokenResult> Handle(GenerateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GenerateEmailConfirmationTokenAsync(request.UserId);
            return new TokenResult(true, [])
            {
                Token = result
            };
        }
    }

}
