using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Commands
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
