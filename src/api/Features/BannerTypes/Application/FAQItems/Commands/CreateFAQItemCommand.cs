using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Models;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.FAQ])]
    public class CreateFAQItemCommand : FAQItemDto, IRequest<Guid>
    {
    }

    public class CreateFAQItemCommandHandler : IRequestHandler<CreateFAQItemCommand, Guid>
    {
        private readonly IEntityRepository<FAQItem> repository;

        public CreateFAQItemCommandHandler(IEntityRepository<FAQItem> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateFAQItemCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new FAQItem
            {
                Id = createdId,
                CategoryId = request.CategoryId,
                Category = default!,
                Answer = request.Answer,
                Question = request.Question,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
