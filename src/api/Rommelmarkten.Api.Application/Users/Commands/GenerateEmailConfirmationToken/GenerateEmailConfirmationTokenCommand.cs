using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;

namespace Rommelmarkten.Api.Application.Users.Commands.GenerateEmailConfirmationToken
{
    public class GenerateEmailConfirmationTokenResult : Result
    {
        public GenerateEmailConfirmationTokenResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public required string Token { get; set; }
    }

    //insecure, so only admins can do this
    [Authorize(Policy=Policies.MustBeAdmin)]
    public class GenerateEmailConfirmationTokenCommand : IRequest<GenerateEmailConfirmationTokenResult>
    {
        public required string UserId { get; set; }
    }

    public class GenerateEmailConfirmationTokenCommandHandler : IRequestHandler<GenerateEmailConfirmationTokenCommand, GenerateEmailConfirmationTokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public GenerateEmailConfirmationTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<GenerateEmailConfirmationTokenResult> Handle(GenerateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateEmailConfirmationTokenAsync(request.UserId);
            return new GenerateEmailConfirmationTokenResult(true, [])
            {
                Token = result
            };
        }
    }

}
