using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.NewsArticles.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.NewsArticles.Requests
{
    public struct GetNewsArticleByIdRequest : IRequest<NewsArticleDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetNewsArticleByIdRequest, NewsArticleDto>
    {
        private readonly IEntityRepository<NewsArticle> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<NewsArticle> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<NewsArticleDto> Handle(GetNewsArticleByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<NewsArticleDto>(entity);
        }
    }

}
