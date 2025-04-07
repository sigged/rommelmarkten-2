using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Application.Models
{
    public class UserDto : IMapFrom<IUser>
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
    }
}
