using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Users.Commands.DeleteUser
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
            this._identityService = identityService;
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
