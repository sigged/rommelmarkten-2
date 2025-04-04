using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.ChangeRole
{

    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class ChangeRoleCommand : IRequest<Result>
    {
        public required string UserId { get; set; }

        public required string RoleId { get; set; }
    }

    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, Result>
    {
        private readonly IUsersDbContext _context;
        private readonly IIdentityService _identityService;

        public ChangeRoleCommandHandler(
            IUsersDbContext context,
            IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<Result> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangeRoleAsync(request.UserId, request.RoleId);
            return result;
        }
    }
}
