using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.FAQCategories.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQCategories.Requests
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
