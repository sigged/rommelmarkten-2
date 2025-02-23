using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.NewsArticles.Models;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.NewsArticles.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
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

            var entity = new NewsArticle {
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
