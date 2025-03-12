using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Application.Users.Queries.GetProfile;

public class UserProfileDto : IMapFrom<UserProfile>
{

    public required string UserId { get; set; }
    public required bool Consented { get; set; }
    public required BlobDto Avatar { get; set; }

}
