using Rommelmarkten.Api.Common.Application.Mappings;

namespace Rommelmarkten.Api.Features.Users.Mapping
{

    /// <summary>
    /// Wires up the IMapFrom<T> implementations in this assembly when scanning the assembly for Profiles
    /// </summary>
    public class UsersMappingProfile : DiscoverIMapFromProfile
    {
        public UsersMappingProfile()
            : base()
        {

        }
    }
}