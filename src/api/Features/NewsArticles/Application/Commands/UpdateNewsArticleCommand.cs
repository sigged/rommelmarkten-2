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
    public class UpdateNewsArticleCommand : NewsArticleDto, IRequest
    {
    }

    public class UpdateNewsArticleCommandHandler : IRequestHandler<UpdateNewsArticleCommand>
    {
        private readonly IEntityRepository<NewsArticle> repository;

        public UpdateNewsArticleCommandHandler(IEntityRepository<NewsArticle> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateNewsArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = new NewsArticle
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                DisplayUntil = request.DisplayUntil
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
