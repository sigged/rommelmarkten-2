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
    public class UpdateFAQItemCommand : FAQItemDto, IRequest
    {
    }

    public class UpdateFAQItemCommandHandler : IRequestHandler<UpdateFAQItemCommand>
    {
        private readonly IEntityRepository<FAQItem> repository;

        public UpdateFAQItemCommandHandler(IEntityRepository<FAQItem> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateFAQItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new FAQItem
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Category = default!,
                Answer = request.Answer,
                Question = request.Question,
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
