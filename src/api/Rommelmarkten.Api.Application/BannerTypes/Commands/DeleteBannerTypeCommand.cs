using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.BannerTypes.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.BannerType])]
    public class DeleteBannerTypeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteBannerTypeCommandHandler : IRequestHandler<DeleteBannerTypeCommand>
    {
        private readonly IEntityRepository<BannerType> repository;

        public DeleteBannerTypeCommandHandler(IEntityRepository<BannerType> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteBannerTypeCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
