using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Users.Models;

namespace Rommelmarkten.Api.Application.Users.Commands.CreateUser
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
