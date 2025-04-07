using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Infrastructure.Identity;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class GetPagedRolesRequest : PaginatedRequest, IRequest<PaginatedList<RoleDto>>
    {
    }

    public class GetPagedRolesRequestHandler : IRequestHandler<GetPagedRolesRequest, PaginatedList<RoleDto>>
    {
        private readonly IConfigurationProvider mapperConfiguration;
        private readonly IUsersDbContext context;
        private readonly IIdentityService identityService;

        public GetPagedRolesRequestHandler(
            IUsersDbContext context, 
            IIdentityService identityService,
            IConfigurationProvider mapperConfiguration)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<RoleDto>> Handle(GetPagedRolesRequest request, CancellationToken cancellationToken)
        {
            var query = (await identityService.GetRolesAsync())
                .OrderBy(e => e.Name)
                .AsQueryable();

            var result = await query.ToPagesAsync<IRole, RoleDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }

}
