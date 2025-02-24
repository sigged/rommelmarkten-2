using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.FAQItems.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Requests
{
    public class GetPagedFAQItemsRequest : PaginatedRequest, IRequest<PaginatedList<FAQItemDto>>
    {
    }

    public class GetPagedFAQItemsRequestValidator : PaginatedRequestValidatorBase<GetPagedFAQItemsRequest>
    {
    }

    public class GetPagedFAQItemsRequestHandler : IRequestHandler<GetPagedFAQItemsRequest, PaginatedList<FAQItemDto>>
    {
        private readonly IEntityRepository<FAQItem> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedFAQItemsRequestHandler(IEntityRepository<FAQItem> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<FAQItemDto>> Handle(GetPagedFAQItemsRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderByDescending(e => e.Order)
            );

            var result = await query.ToPagesAsync<FAQItem, FAQItemDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }
}
