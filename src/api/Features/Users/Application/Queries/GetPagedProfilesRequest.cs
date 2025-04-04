using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Application.Models;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class GetPagedProfilesRequest : PaginatedRequest, IRequest<PaginatedList<UserProfileDto>>
    {
    }

    public class GetPagedProfilesRequestValidator : PaginatedRequestValidatorBase<GetPagedProfilesRequest>
    {
    }

    public class GetPagedProfilesRequestHandler : IRequestHandler<GetPagedProfilesRequest, PaginatedList<UserProfileDto>>
    {
        private readonly IConfigurationProvider mapperConfiguration;
        private readonly IUsersDbContext context;

        public GetPagedProfilesRequestHandler(IUsersDbContext context, IConfigurationProvider mapperConfiguration)
        {
            this.context = context;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<UserProfileDto>> Handle(GetPagedProfilesRequest request, CancellationToken cancellationToken)
        {
            var query = context.Set<ApplicationUser>()
                .Join(context.Set<UserProfile>(),
                    user => user.Id,
                    profile => profile.OwnedBy,
                    (user, userProfile) => new UserProfileAndUser
                    {
                        User = user,
                        Profile = userProfile
                    })
                .OrderBy(e => e.Profile.Created);


            var result = await query.ToPagesAsync<UserProfileAndUser, UserProfileDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


    public class UserProfileAndUser
    {
        public required ApplicationUser User { get; init; }
        public required UserProfile Profile { get; init; }
    }

    public class UserProfileAndUserAndRole
    {
        public required IdentityRole Role { get; init; }
        public required UserProfileAndUser UserProfileAndUser { get; init; }
    }
}
