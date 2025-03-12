using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser
{


    public class CreateUserResult : Result
    {
        public CreateUserResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {

        }

        public required string? RegisteredUserId { get; set; }

    }

    public class CreateUserCommand : IRequest<CreateUserResult>
    {
        public required string Name { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required string Captcha { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IUsersDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IAvatarGenerator _avatarGenerator;
        private readonly ICaptchaProvider captchaProvider;

        public CreateUserCommandHandler(IUsersDbContext context, IIdentityService identityService, IAvatarGenerator avatarGenerator, ICaptchaProvider captchaProvider)
        {
            _context = context;
            _identityService = identityService;
            _avatarGenerator = avatarGenerator;
            this.captchaProvider = captchaProvider;
        }

        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var captchaResult = await captchaProvider.VerifyChallenge(request.Captcha);
            if (!captchaResult.Succeeded)
            {
                return new CreateUserResult(false, captchaResult.Errors)
                {
                    RegisteredUserId = null
                };
            }
            else
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

                    return new CreateUserResult(true, [])
                    {
                        RegisteredUserId = user.Id
                    };
                }
                else
                {
                    return new CreateUserResult(false, result.Result.Errors)
                    {
                        RegisteredUserId = null
                    };
                }
            }
        }
    }
}
