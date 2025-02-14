using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;

namespace Rommelmarkten.Api.Application.Users.Queries.GetProfile
{
    [Authorize]
    public class GetAvatarQuery : IRequest<BlobDto>
    {
        public required string UserId { get; set; }
    }

    public class GetAvatarQueryHandler : IRequestHandler<GetAvatarQuery, BlobDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAvatarQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlobDto?> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserProfiles
                .Include(e => e.Avatar)
                .Where(e => e.UserId.Equals(request.UserId))
                .Select(e => e.Avatar)
                .ProjectTo<BlobDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
