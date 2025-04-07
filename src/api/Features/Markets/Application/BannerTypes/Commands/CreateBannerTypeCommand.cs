using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Commands
{
    [Authorize(Policy=CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.BannerType])]
    public class CreateBannerTypeCommand : BannerTypeDto, IRequest<Guid>
    {
    }

    public class CreateBannerTypeCommandHandler : IRequestHandler<CreateBannerTypeCommand, Guid>
    {
        private readonly IEntityRepository<BannerType> repository;

        public CreateBannerTypeCommandHandler(IEntityRepository<BannerType> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateBannerTypeCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new BannerType {
                Id = createdId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
