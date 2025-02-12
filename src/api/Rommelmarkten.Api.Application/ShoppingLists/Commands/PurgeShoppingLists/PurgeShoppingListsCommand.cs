using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.PurgeShoppingLists
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    public class PurgeShoppingListsCommand : IRequest
    {
    }
    public class PurgeShoppingListsCommandHandler : IRequestHandler<PurgeShoppingListsCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeShoppingListsCommandHandler(IApplicationDbContext context)
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
