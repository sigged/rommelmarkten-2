using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.NewsArticles.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
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
