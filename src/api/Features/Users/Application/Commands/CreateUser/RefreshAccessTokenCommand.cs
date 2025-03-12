using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser
{

    public class RefreshAccessTokenCommand : IRequest<AccessTokenResult>
    {
        public required string RefreshToken { get; set; }
    }

    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, AccessTokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public RefreshAccessTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<AccessTokenResult> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
