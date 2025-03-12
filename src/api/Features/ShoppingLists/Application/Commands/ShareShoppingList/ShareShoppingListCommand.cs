using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.ShareShoppingList
{
    [Authorize]
    public class ShareShoppingListCommand : IRequest
    {
        public int Id { get; set; }

        public required string AssociateUserName { get; set; }
    }

    public class ShareShoppingListCommandHandler : IRequestHandler<ShareShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public ShareShoppingListCommandHandler(IShoppingListsDbContext context, IIdentityService identityService, IDomainEventService domainEventService, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _identityService = identityService;
            _domainEventService = domainEventService;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(ShareShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists.FindAsync(request.Id);
            var associate = await _identityService.FindByName(request.AssociateUserName);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.Id);
            }
            if (associate == null)
            {
                throw new NotFoundException(nameof(IUser), request.AssociateUserName);
            }
            if (!await _resourceAuthorizationService.AuthorizeAny(entity, CorePolicies.MustBeCreator, CorePolicies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }
            if (await _context.ListAssociates.AnyAsync(o => o.AssociateId.Equals(associate.Id)))
            {
                throw new NotFoundException("The list is already shared with this user");
            }

            var shareEntry = new ListAssociate
            {
                ListId = request.Id,
                AssociateId = associate.Id,
                AssociatedOn = DateTime.UtcNow
            };

            await _context.ListAssociates.AddAsync(shareEntry);

            entity.DomainEvents.Append(new ShoppingListShareEvent(entity, associate, true));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
