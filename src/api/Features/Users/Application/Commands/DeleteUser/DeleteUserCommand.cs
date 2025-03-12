using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Commands.DeleteUser
{
    [Authorize(Policy = Policies.MustBeCreatorOrAdmin)]
    public class DeleteUserCommand : IRequest<Result>
    {
        public required string UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IIdentityService _identityService;
        private readonly IEntityRepository<UserProfile> profileRepository;

        public DeleteUserCommandHandler(IIdentityService identityService, IEntityRepository<UserProfile> profileRepository)
        {
            _identityService = identityService;
            this.profileRepository = profileRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteUserAsync(request.UserId);
            if (result.Succeeded)
            {
                await profileRepository.DeleteByIdAsync(request.UserId);
                return Result.Success();
            }
            else
            {
                return result;
            }
        }
    }
}
