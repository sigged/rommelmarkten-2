using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.NewsArticles.Models;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.NewsArticles.Commands.Update
{

    [Authorize(Policy = Policies.MustBeAdmin)]
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
