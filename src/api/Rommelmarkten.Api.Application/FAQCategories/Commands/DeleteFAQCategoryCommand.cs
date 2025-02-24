using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQCategories.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
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
