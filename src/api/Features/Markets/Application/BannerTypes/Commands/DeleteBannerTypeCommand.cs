using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.BannerType])]
    public class DeleteBannerTypeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteBannerTypeCommandHandler : IRequestHandler<DeleteBannerTypeCommand>
    {
        private readonly IEntityRepository<BannerType> repository;

        public DeleteBannerTypeCommandHandler(IEntityRepository<BannerType> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteBannerTypeCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
