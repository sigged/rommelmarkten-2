using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ForceLogout
{

    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class ForceLogoutCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
    }

    public class ForceLogoutCommandHandler : IRequestHandler<ForceLogoutCommand, Result>
    {
        private readonly IUsersDbContext _context;
        private readonly IIdentityService _identityService;

        public ForceLogoutCommandHandler(
            IUsersDbContext context,
            IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<Result> Handle(ForceLogoutCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
