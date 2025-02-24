using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.FAQCategories.Models;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.FAQCategories.Commands
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
