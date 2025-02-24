using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.FAQ])]
    public class DeleteFAQItemCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteFAQItemCommandHandler : IRequestHandler<DeleteFAQItemCommand>
    {
        private readonly IEntityRepository<FAQItem> repository;

        public DeleteFAQItemCommandHandler(IEntityRepository<FAQItem> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteFAQItemCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
