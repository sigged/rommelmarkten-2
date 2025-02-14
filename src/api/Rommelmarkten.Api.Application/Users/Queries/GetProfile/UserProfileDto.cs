using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Users.Queries.GetProfile;

public class UserProfileDto : IMapFrom<UserProfile>
{

    public required string UserId { get; set; }
    public required bool Consented { get; set; }
    public required BlobDto Avatar { get; set; }

}
