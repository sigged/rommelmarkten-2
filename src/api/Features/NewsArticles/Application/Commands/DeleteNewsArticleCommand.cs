using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.NewsArticle])]
    public class DeleteNewsArticleCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteNewsArticleCommandHandler : IRequestHandler<DeleteNewsArticleCommand>
    {
        private readonly IEntityRepository<NewsArticle> repository;

        public DeleteNewsArticleCommandHandler(IEntityRepository<NewsArticle> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteNewsArticleCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
