using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Models
{
    public class UserDto : IMapFrom<IUser>
    {
        public required string Id { get; set; }

        public string? Email { get; set; }
    }
}
