using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Affiliates.Application.Models;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
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

            var entity = new AffiliateAd
            {
                Id = createdId,
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
