using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Application.Users.Queries.GetProfile;

public class UserProfileDto : IMapFrom<UserProfile>
{

    public string UserId { get; set; }
    public bool Consented { get; set; }
    public BlobDto Avatar { get; set; }

}
