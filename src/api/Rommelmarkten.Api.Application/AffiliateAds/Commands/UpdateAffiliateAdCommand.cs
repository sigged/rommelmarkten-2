using MediatR;
using Rommelmarkten.Api.Application.AffiliateAds.Models;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Affiliates;

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
