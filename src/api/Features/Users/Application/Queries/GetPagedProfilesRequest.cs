using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Application.Security;
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
        private readonly IEntityRepository<UserProfile> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedProfilesRequestHandler(IEntityRepository<UserProfile> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<UserProfileDto>> Handle(GetPagedProfilesRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderByDescending(e => e.Created)
            );

            var result = await query.ToPagesAsync<UserProfile, UserProfileDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
