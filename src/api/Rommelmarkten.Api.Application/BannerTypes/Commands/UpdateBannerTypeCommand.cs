using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.BannerTypes.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.BannerTypes.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.BannerType])]
    public class UpdateBannerTypeCommand : BannerTypeDto, IRequest
    {
    }

    public class UpdateBannerTypeCommandHandler : IRequestHandler<UpdateBannerTypeCommand>
    {
        private readonly IEntityRepository<BannerType> repository;

        public UpdateBannerTypeCommandHandler(IEntityRepository<BannerType> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateBannerTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new BannerType
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
