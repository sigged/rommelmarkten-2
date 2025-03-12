using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Models;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Requests
{
    public class GetPagedFAQCategoriesRequest : PaginatedRequest, IRequest<PaginatedList<FAQCategoryDto>>
    {
    }

    public class GetPagedFAQCategoriesRequestValidator : PaginatedRequestValidatorBase<GetPagedFAQCategoriesRequest>
    {
    }

    public class GetPagedFAQCategorysRequestHandler : IRequestHandler<GetPagedFAQCategoriesRequest, PaginatedList<FAQCategoryDto>>
    {
        private readonly IEntityRepository<FAQCategory> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedFAQCategorysRequestHandler(IEntityRepository<FAQCategory> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<FAQCategoryDto>> Handle(GetPagedFAQCategoriesRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderBy(e => e.Name)
            );

            var result = await query.ToPagesAsync<FAQCategory, FAQCategoryDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }

}
