using MediatR;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    //insecure, so only admins can do this
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class GeneratePasswordResetCommand : IRequest<TokenResult>
    {
        public required string UserId { get; set; }
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
            var user = await _identityService.FindById(request.UserId);

            var result = await _identityService.GeneratePasswordResetTokenAsync(user.Email!);
            return new TokenResult(true, [])
            {
                Token = result
            };
        }
    }
}
