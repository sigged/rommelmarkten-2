using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Application.Security;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.UpdateProfile
{

    [AuthorizeResource(Policy = UsersPolicies.MustBeSelfOrAdmin, 
                       IdentifierPropertyName = nameof(UserId), 
                       ResourceType = typeof(UserProfile))]
    public class UpdateProfileCommand : IRequest
    {
        public required string UserId { get; set; }
        public bool HasConsented { get; set; }

    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IUsersDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateProfileCommandHandler(
            IUsersDbContext context, 
            IIdentityService identityService, 
            ICurrentUserService currentUserService,
            IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _identityService = identityService;
            _currentUserService = currentUserService;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserProfiles
                .SingleOrDefaultAsync(e => e.OwnedBy.Equals(request.UserId));

            if (entity == null)
                throw new NotFoundException(nameof(UserProfile), nameof(UserProfile.OwnedBy));

            entity.Consented = request.HasConsented;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
