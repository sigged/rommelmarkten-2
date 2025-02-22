using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Affiliates;

namespace Rommelmarkten.Api.Application.AffiliateAds.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.AffiliateAd])]
    public class DeleteAffiliateAdCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteAffiliateAdCommandHandler : IRequestHandler<DeleteAffiliateAdCommand>
    {
        private readonly IEntityRepository<AffiliateAd> repository;

        public DeleteAffiliateAdCommandHandler(IEntityRepository<AffiliateAd> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteAffiliateAdCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
