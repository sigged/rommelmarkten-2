using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Application.Models;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    [Authorize]
    public class GetCurrentUserProfileQuery : IRequest<UserProfileDto>
    {
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetCurrentUserProfileQuery, UserProfileDto?>
    {
        private readonly IUsersDbContext context;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetUserProfileQueryHandler(IUsersDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.context = context;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<UserProfileDto?> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await context.Set<ApplicationUser>()
               .Join(context.Set<UserProfile>(),
                   user => user.Id,
                   profile => profile.OwnedBy,
                   (user, userProfile) => new UserProfileAndUser
                   {
                       User = user,
                       Profile = userProfile
                   })
               .ProjectTo<UserProfileDto>(mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(e => e.Id == currentUserService.UserId);

            if(userProfile != null)
            {
                return userProfile;
            }
            else
            {
                throw new NotFoundException($"Your profile could not be found");
            }
        }
    }
}
