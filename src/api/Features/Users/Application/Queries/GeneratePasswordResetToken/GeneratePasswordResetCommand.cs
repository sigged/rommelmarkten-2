using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries.GeneratePasswordResetToken
{
    //insecure, so only admins can do this
    [Authorize(Policy = Policies.MustBeAdmin)]
    public class GeneratePasswordResetCommand : IRequest<TokenResult>
    {
        public required string Email { get; set; }
    }
    public class GeneratePasswordResetCommandHandler : IRequestHandler<GeneratePasswordResetCommand, TokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public GeneratePasswordResetCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<TokenResult> Handle(GeneratePasswordResetCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GeneratePasswordResetTokenAsync(request.Email);
            return new TokenResult(true, [])
            {
                Token = result
            };
        }
    }
}
