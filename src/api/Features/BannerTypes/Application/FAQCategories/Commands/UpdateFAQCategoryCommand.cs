using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Models;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.FAQ])]
    public class UpdateFAQCategoryCommand : FAQCategoryDto, IRequest
    {
    }

    public class UpdateFAQCategoryCommandHandler : IRequestHandler<UpdateFAQCategoryCommand>
    {
        private readonly IEntityRepository<FAQCategory> repository;

        public UpdateFAQCategoryCommandHandler(IEntityRepository<FAQCategory> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateFAQCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new FAQCategory
            {
                Id = request.Id,
                Name = request.Name,
                Order = request.Order
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
