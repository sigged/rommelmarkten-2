using Rommelmarkten.Api.Common.Application.Mappings;

namespace Rommelmarkten.Api.Features.Affiliates.Mapping
{

    /// <summary>
    /// Wires up the IMapFrom<T> implementations in this assembly when scanning the assembly for Profiles
    /// </summary>
    public class AffiliateMappingProfile : DiscoverIMapFromProfile
    {
        public AffiliateMappingProfile()
            : base()
        {
           
        }
    }
}