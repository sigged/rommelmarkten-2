using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.FAQItems.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
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
