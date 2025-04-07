using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Models;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.FAQ])]
    public class CreateFAQCategoryCommand : FAQCategoryDto, IRequest<Guid>
    {
    }

    public class CreateFAQCategoryCommandHandler : IRequestHandler<CreateFAQCategoryCommand, Guid>
    {
        private readonly IEntityRepository<FAQCategory> repository;

        public CreateFAQCategoryCommandHandler(IEntityRepository<FAQCategory> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateFAQCategoryCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new FAQCategory
            {
                Id = createdId,
                Name = request.Name,
                Order = request.Order
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
