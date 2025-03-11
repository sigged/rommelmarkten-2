using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.Users.Models;

namespace Rommelmarkten.Api.Application.Users.Commands.GeneratePasswordResetToken
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
