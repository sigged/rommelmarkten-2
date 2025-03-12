using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.Users.Application.Queries.GetAvatar
{
    [Authorize]
    public class GetAvatarQuery : IRequest<BlobDto>
    {
        public required string UserId { get; set; }
    }

    public class GetAvatarQueryHandler : IRequestHandler<GetAvatarQuery, BlobDto?>
    {
        private readonly IUsersDbContext _context;
        private readonly IMapper _mapper;

        public GetAvatarQueryHandler(IUsersDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlobDto?> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult((BlobDto?)null);
            //return await _context.UserProfiles
            //    //.Include(e => e.Avatar)
            //    .Where(e => e.UserId.Equals(request.UserId))
            //    .Select(e => e.Avatar)
            //    .ProjectTo<BlobDto>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
