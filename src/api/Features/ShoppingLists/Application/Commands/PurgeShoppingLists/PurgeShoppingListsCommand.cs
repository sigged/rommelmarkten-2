using MediatR;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.PurgeShoppingLists
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    public class PurgeShoppingListsCommand : IRequest
    {
    }
    public class PurgeShoppingListsCommandHandler : IRequestHandler<PurgeShoppingListsCommand>
    {
        private readonly IShoppingListsDbContext _context;

        public PurgeShoppingListsCommandHandler(IShoppingListsDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PurgeShoppingListsCommand request, CancellationToken cancellationToken)
        {
            _context.ShoppingLists.RemoveRange(_context.ShoppingLists);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
