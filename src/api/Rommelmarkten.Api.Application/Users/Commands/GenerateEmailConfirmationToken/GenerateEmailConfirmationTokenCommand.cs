using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.Users.Commands.GenerateEmailConfirmationToken
{

    public class GenerateEmailConfirmationTokenCommand : IRequest<string>
    {
        public required string UserId { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<GenerateEmailConfirmationTokenCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ConfirmEmailCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<string> Handle(GenerateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateEmailConfirmationTokenAsync(request.UserId);
            return result;
        }
    }
}
