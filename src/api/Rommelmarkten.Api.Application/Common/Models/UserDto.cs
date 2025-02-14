using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Common.Models
{
    public class UserDto : IMapFrom<IUser>
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
    }
}
