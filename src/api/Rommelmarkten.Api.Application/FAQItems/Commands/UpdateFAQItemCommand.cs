using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.FAQItems.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
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
