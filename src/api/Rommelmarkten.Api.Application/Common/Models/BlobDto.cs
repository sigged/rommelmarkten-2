using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Application.Common.Models
{
    public record BlobDto : IMapFrom<Blob>
    {
        public required string Name { get; set; }

        public required byte[] Content { get; set; }

        public required string Type { get; set; }
    }
}
