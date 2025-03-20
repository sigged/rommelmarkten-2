using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Models;

public class UserProfileDto : IMapFrom<UserProfile>
{

    public required string OwnedBy { get; set; }
    public required bool Consented { get; set; }
    public required BlobDto Avatar { get; set; }

}
