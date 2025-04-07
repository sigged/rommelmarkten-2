using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Application.Models
{
    public class RoleDto : IMapFrom<IRole>
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
    }
}
