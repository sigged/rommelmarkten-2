using AutoMapper;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists
{
    [Authorize]
    public class GetShoppingListsQuery : IRequest<IList<ShoppingListDto>>
    {
    }

    public class GetShoppingListsHandler : IRequestHandler<GetShoppingListsQuery, IList<ShoppingListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetShoppingListsHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IIdentityService identityService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<IList<ShoppingListDto>> Handle(GetShoppingListsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ShoppingLists
                    .Include(e => e.Associates)
                    .Include(e => e.Items)
                    .Where(e =>
                        e.CreatedBy.Equals(_currentUserService.UserId) ||
                        e.Associates.Select(a => a.AssociateId).Contains(_currentUserService.UserId)
                    )
                    .AsNoTracking()
                    .Join(
                        _identityService.GetUsers(),
                        shoppingList => shoppingList.CreatedBy, user => user.Id, (shoppingList, user) => new { ShoppingList = shoppingList, User = user }
                    )
                    .Select(e => new ShoppingListDto
                    {
                        Id = e.ShoppingList.Id,
                        Title = e.ShoppingList.Title,
                        Color = e.ShoppingList.Color,
                        Created = e.ShoppingList.Created,
                        LastModified = e.ShoppingList.LastModified,
                        Owner = e.User.UserName,
                        IsShared = !e.User.Id.Equals(_currentUserService.UserId),
                    })
                    //.ProjectTo<ShoppingListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken);
        }
    }
}
