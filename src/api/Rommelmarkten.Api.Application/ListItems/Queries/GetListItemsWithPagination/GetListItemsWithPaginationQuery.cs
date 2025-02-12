using AutoMapper;
using AutoMapper.QueryableExtensions;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ListItems.Queries.GetListItemsWithPagination
{
    [Authorize]
    public class GetListItemsWithPaginationQuery : IRequest<PaginatedList<ListItemDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetListItemsWithPaginationQueryHandler : IRequestHandler<GetListItemsWithPaginationQuery, PaginatedList<ListItemDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetListItemsWithPaginationQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper, IResourceAuthorizationService resourceAuthorizationService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<PaginatedList<ListItemDto>> Handle(GetListItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists
                .Include(e => e.Associates)
                .SingleOrDefaultAsync(e => e.Id.Equals(request.ListId));

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustHaveListAccess, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            return await _context.ListItems
                .Where(x => 
                    x.ListId == request.ListId
                )
                .OrderBy(x => x.Title)
                .ProjectTo<ListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
