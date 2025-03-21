using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Application.Models;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    [Authorize]
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto?>
    {
        private readonly IUsersDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IUsersDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<UserProfileDto?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            //var usersWithProfiles = await _context.Set<ApplicationUser>()
            //    .Join(_context.Set<UserProfile>(),
            //        user => user.Id,
            //        profile => profile.OwnedBy,
            //        (user, profile) => new UserProfileDto
            //        { 
            //            OwnedBy = profile.OwnedBy,
            //            Consented = profile.Consented,
            //            //Avatar = null

            //            //Id = user.Id,
            //            //Email = user.Email,
            //            //UserName = user.UserName,
            //            //Consented = profile.Consented,
            //            //Email = profile.ActivationRemindersSent,
            //            //profile.
            //            //Avatar = new BlobDto
            //            //{
            //            //    Content = profile.Avatar.Content,
            //            //    ContentType = profile.Avatar.ContentType
            //            //}
            //        })
            //    .Where(e => e.OwnedBy.Equals(_currentUserService.UserId))
            //    .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(cancellationToken);

            //return usersWithProfiles;
            ////return await _context.UserProfiles
            ////    .Where(e => e.OwnedBy.Equals(_currentUserService.UserId))
            ////    .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            ////    .FirstOrDefaultAsync(cancellationToken);
            ///
            return null;
        }
    }
}
