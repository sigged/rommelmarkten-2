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

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.DeleteShoppingList
{

    [Authorize]
    public class DeleteShoppingListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteShoppingListCommandHandler : IRequestHandler<DeleteShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public DeleteShoppingListCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.Id);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            _context.ShoppingLists.Remove(entity);

            entity.DomainEvents.Append(new ShoppingListRemovedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
