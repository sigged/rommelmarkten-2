using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Features.NewsArticles.Application.Models;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Requests
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
