using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Users.Commands.CreateUser
{
    public class RefreshAccessTokenResult : Result
    {
        public RefreshAccessTokenResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public required string TokenType { get; set; }

        public required string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public required string RefreshToken { get; set; }
    }

    public class RefreshAccessTokenCommand : IRequest<RefreshAccessTokenResult>
    {
        public required string RefreshToken { get; set; }
    }

    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, RefreshAccessTokenResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public RefreshAccessTokenCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<RefreshAccessTokenResult> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
