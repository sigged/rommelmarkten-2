using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Application.Users.Commands.UpdateAvatar
{
    public class UpdateAvatarCommand : IRequest
    {
        public BlobDto? Avatar { get; set; }
    }

    public class UpdateAvatarCommandHandler : IRequestHandler<UpdateAvatarCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAvatarGenerator _avatarGenerator;

        public UpdateAvatarCommandHandler(IApplicationDbContext context, IIdentityService identityService, ICurrentUserService currentUserService, IAvatarGenerator avatarGenerator)
        {
            _context = context;
            _identityService = identityService;
            _currentUserService = currentUserService;
            _avatarGenerator = avatarGenerator;
        }

        public async Task Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserProfiles
                .SingleOrDefaultAsync(e => e.UserId.Equals(_currentUserService.UserId));

            if(entity != null)
            {
                if (request.Avatar == null)
                {
                    var userName = await _identityService.GetUserNameAsync(_currentUserService.UserId);


                    if (userName == null)
                        throw new NotFoundException(nameof(IUser), nameof(IUser.UserName));

                    var user = await _identityService.FindByName(userName);
                    //entity.Avatar = await _avatarGenerator.GenerateAvatar(user);
                }
                else
                {
                    //entity.Avatar = new Blob(
                    //    request.Avatar.Name, 
                    //    request.Avatar.Type, 
                    //    request.Avatar.Content);
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new NotFoundException(nameof(UserProfile), nameof(UserProfile.UserId));
            }
        }
    }
}
