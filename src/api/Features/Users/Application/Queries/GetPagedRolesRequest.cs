using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    //[Authorize(Policy = CorePolicies.MustBeAdmin)]
    //public class GetPagedRolesRequest : PaginatedRequest, IRequest<PaginatedList<RoleDto>>
    //{
    //}

    //public class GetPagedRolesRequestHandler : IRequestHandler<GetPagedRolesRequest, PaginatedList<RoleDto>>
    //{
    //    private readonly IConfigurationProvider mapperConfiguration;
    //    private readonly IUsersDbContext context;

    //    public GetPagedRolesRequestHandler(IUsersDbContext context, IConfigurationProvider mapperConfiguration)
    //    {
    //        this.context = context;
    //        this.mapperConfiguration = mapperConfiguration;
    //    }

    //    public async Task<PaginatedList<RoleDto>> Handle(GetPagedRolesRequest request, CancellationToken cancellationToken)
    //    {
    //        var query = context.Set<IdentityRole>()
    //            .OrderBy(e => e.Name);

    //        var result = await query.ToPagesAsync<IdentityRole, RoleDto>(request.PageNumber, request.PageSize, mapperConfiguration);
    //        return result;
    //    }
    //}

}
