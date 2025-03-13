using Rommelmarkten.Api.Common.Application.Mappings;

namespace Rommelmarkten.Api.Features.Markets.Mapping
{

    /// <summary>
    /// Wires up the IMapFrom<T> implementations in this assembly when scanning the assembly for Profiles
    /// </summary>
    public class MarketMappingProfile : DiscoverIMapFromProfile
    {
        public MarketMappingProfile()
            : base()
        {

        }
    }
}