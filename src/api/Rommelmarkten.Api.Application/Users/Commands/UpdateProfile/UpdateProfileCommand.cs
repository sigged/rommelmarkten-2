﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Users.Commands.UpdateProfile
{

    [Authorize(Policy = Policies.MustBeCreatorOrAdmin)]
    public class UpdateProfileCommand : IRequest
    {
        public bool HasConsented { get; set; }

    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateProfileCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUserService)
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
