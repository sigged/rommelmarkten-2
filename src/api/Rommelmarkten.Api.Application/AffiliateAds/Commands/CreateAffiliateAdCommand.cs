using MediatR;
using Rommelmarkten.Api.Application.AffiliateAds.Models;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Affiliates;

namespace Rommelmarkten.Api.Application.AffiliateAds.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.AffiliateAd])]
    public class CreateAffiliateAdCommand : AffiliateAdDto, IRequest<Guid>
    {
    }

    public class CreateAffiliateAdCommandHandler : IRequestHandler<CreateAffiliateAdCommand, Guid>
    {
        private readonly IEntityRepository<AffiliateAd> repository;

        public CreateAffiliateAdCommandHandler(IEntityRepository<AffiliateAd> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateAffiliateAdCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new AffiliateAd {
                Id = request.Id,
                ImageUrl = request.ImageUrl,
                AffiliateName = request.AffiliateName,
                AffiliateURL = request.AffiliateURL,
                AdContent = request.AdContent,
                Order = request.Order,
                IsActive = request.IsActive,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
