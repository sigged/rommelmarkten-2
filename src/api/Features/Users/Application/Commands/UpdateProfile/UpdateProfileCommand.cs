using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.UpdateProfile
{

    //[Authorize(Policy = CorePolicies.MustBeSelfOrAdmin)]
    [Authorize]
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
                .SingleOrDefaultAsync(e => e.UserId.Equals(request.UserId));

            IUser user = await _identityService.FindById(request.UserId!);

            if (entity == null)
                throw new NotFoundException(nameof(UserProfile), nameof(UserProfile.UserId));

            if (user == null)
                throw new NotFoundException(nameof(IUser), nameof(IUser.Id));

            if(!await _resourceAuthorizationService.AuthorizeAny(user, CorePolicies.MustBeSelfOrAdmin))
            {
                throw new ForbiddenAccessException();
            }


            entity.Consented = request.HasConsented;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
