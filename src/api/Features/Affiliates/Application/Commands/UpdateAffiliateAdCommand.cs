using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Affiliates.Application.Models;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Application.AffiliateAds.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.AffiliateAd])]
    public class UpdateAffiliateAdCommand : AffiliateAdDto, IRequest
    {
    }

    public class UpdateAffiliateAdCommandHandler : IRequestHandler<UpdateAffiliateAdCommand>
    {
        private readonly IEntityRepository<AffiliateAd> repository;

        public UpdateAffiliateAdCommandHandler(IEntityRepository<AffiliateAd> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateAffiliateAdCommand request, CancellationToken cancellationToken)
        {
            var entity = new AffiliateAd
            {
                Id = request.Id,
                ImageUrl = request.ImageUrl,
                AffiliateName = request.AffiliateName,
                AffiliateURL = request.AffiliateURL,
                AdContent = request.AdContent,
                Order = request.Order,
                IsActive = request.IsActive,
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
