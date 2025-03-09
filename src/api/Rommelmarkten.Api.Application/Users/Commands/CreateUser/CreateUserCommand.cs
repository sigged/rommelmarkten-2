using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Users.Queries.CreateUser;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Users.Commands.CreateUser
{

    public class CreateUserCommand : IRequest<CreateUserResult>
    {
        public required string Name { get; set; } 

        public required string UserName { get; set; }

        public required string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
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

        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, request.Password);
            if (result.Result.Succeeded)
            {
                var user = await _identityService.FindByName(request.UserName);
                //var avatar = await _avatarGenerator.GenerateAvatar(user);

                var profile = new UserProfile
                {
                    UserId = user.Id,
                    Consented = false,
                    Name = request.Name
                    //Avatar = avatar
                };
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();

                return new CreateUserResult(true, []) { 
                     RegisteredUserId = user.Id
                };
            }
            else
            {
                return new CreateUserResult(false, result.Result.Errors) { 
                    RegisteredUserId = null
                };
            }
        }
    }
}
