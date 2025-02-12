using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IAvatarGenerator _avatarGenerator;

        public CreateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService, IAvatarGenerator avatarGenerator)
        {
            _context = context;
            _identityService = identityService;
            _avatarGenerator = avatarGenerator;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, request.Password);
            if (result.Result.Succeeded)
            {
                var user = await _identityService.FindByName(request.UserName);
                var avatar = await _avatarGenerator.GenerateAvatar(user);

                var profile = new UserProfile
                {
                    UserId = user.Id,
                    Consented = false,
                    Avatar = avatar
                };
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();

                return result.UserId;
            }
            else
            {
                return null;
            }
        }
    }
}
