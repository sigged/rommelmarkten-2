using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Application.Common.Models
{
    public record BlobDto : IMapFrom<Blob>
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string Type { get; set; }
    }
}
