using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.NewsArticles.Application.Models;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.NewsArticle])]
    public class CreateNewsArticleCommand : NewsArticleDto, IRequest<Guid>
    {
    }

    public class CreateNewsArticleCommandHandler : IRequestHandler<CreateNewsArticleCommand, Guid>
    {
        private readonly IEntityRepository<NewsArticle> repository;

        public CreateNewsArticleCommandHandler(IEntityRepository<NewsArticle> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateNewsArticleCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new NewsArticle
            {
                Id = createdId,
                Title = request.Title,
                Content = request.Content,
                DisplayUntil = request.DisplayUntil
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
