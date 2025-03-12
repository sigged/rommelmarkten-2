using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
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
