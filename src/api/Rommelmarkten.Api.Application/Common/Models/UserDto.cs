using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Common.Models
{
    public class UserDto : IMapFrom<IUser>
    {
        string Id { get; set; }
        string UserName { get; set; }
    }
}
