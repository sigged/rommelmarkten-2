using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.NewsArticles.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.NewsArticles.Requests
{
    public class GetPagedNewsArticlesRequest : PaginatedRequest, IRequest<PaginatedList<NewsArticleDto>>
    {
    }

    public class GetPagedNewsArticlesRequestValidator : PaginatedRequestValidatorBase<GetPagedNewsArticlesRequest>
    {
    }

    public class GetPagedNewsArticlesRequestHandler : IRequestHandler<GetPagedNewsArticlesRequest, PaginatedList<NewsArticleDto>>
    {
        private readonly IEntityRepository<NewsArticle> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedNewsArticlesRequestHandler(IEntityRepository<NewsArticle> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<NewsArticleDto>> Handle(GetPagedNewsArticlesRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderByDescending(e => e.Created)
            );

            var result = await query.ToPagesAsync<NewsArticle, NewsArticleDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
