using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ListItems.Commands.DeleteListItem
{

    [Authorize]
    public class DeleteListItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteListItemCommandHandler : IRequestHandler<DeleteListItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public DeleteListItemCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(DeleteListItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ListItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ListItem), request.Id);
            }

            var parentList = await _context.ShoppingLists
               .Include(e => e.Associates)
               .SingleOrDefaultAsync(e => e.Id.Equals(entity.ListId));

            if (!await _resourceAuthorizationService.AuthorizeAny(parentList, Policies.MustHaveListAccess, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            _context.ListItems.Remove(entity);

            entity.DomainEvents.Append(new ListItemRemovedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
