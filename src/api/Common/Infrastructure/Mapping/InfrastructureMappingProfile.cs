using Rommelmarkten.Api.Common.Application.Mappings;

namespace Rommelmarkten.Api.Common.Infrastructure.Mapping
{
    /// <summary>
    /// Wires up the IMapFrom<T> implementations in this assembly when scanning the assembly for Profiles
    /// </summary>
    public class InfrastructureMappingProfile : DiscoverIMapFromProfile
    {
        public InfrastructureMappingProfile() 
            : base()
        {
            
        }
    }
}