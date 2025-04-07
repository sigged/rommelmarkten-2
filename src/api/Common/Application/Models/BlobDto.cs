using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Domain.ValueObjects;

namespace Rommelmarkten.Api.Common.Application.Models
{
    public record BlobDto : IMapFrom<Blob>
    {
        public required string Name { get; set; }

        public required byte[] Content { get; set; }

        public required string Type { get; set; }
    }
}
