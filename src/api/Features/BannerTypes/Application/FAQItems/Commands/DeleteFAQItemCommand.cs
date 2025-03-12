using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
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
