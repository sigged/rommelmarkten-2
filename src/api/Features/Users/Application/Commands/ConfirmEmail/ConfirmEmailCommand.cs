using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ConfirmEmail
{

    public class ConfirmEmailCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
        public required string ConfirmationToken { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public ConfirmEmailCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ConfirmEmailAsync(request.UserId, request.ConfirmationToken);
            return result;
        }
    }
}
