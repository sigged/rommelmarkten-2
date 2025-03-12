using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.UpdateProfile
{

    [Authorize(Policy = CorePolicies.MustBeCreatorOrAdmin)]
    public class UpdateProfileCommand : IRequest
    {
        public bool HasConsented { get; set; }

    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IUsersDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProfileCommandHandler(IUsersDbContext context, IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _context = context;
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserProfiles
                .SingleOrDefaultAsync(e => e.UserId.Equals(_currentUserService.UserId));

            if (entity == null)
                throw new NotFoundException(nameof(UserProfile), nameof(UserProfile.UserId));

            entity.Consented = request.HasConsented;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
