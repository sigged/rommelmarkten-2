using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.FAQCategories.Models;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQCategories.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
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
