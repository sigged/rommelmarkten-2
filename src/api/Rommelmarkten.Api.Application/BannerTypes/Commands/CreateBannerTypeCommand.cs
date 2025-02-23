using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.BannerTypes.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.BannerTypes.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
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
