using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.FAQ])]
    public class DeleteFAQCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteFAQCategoryCommandHandler : IRequestHandler<DeleteFAQCategoryCommand>
    {
        private readonly IEntityRepository<FAQCategory> repository;

        public DeleteFAQCategoryCommandHandler(IEntityRepository<FAQCategory> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteFAQCategoryCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
