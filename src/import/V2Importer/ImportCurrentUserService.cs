using Rommelmarkten.Api.Application.Common.Interfaces;

namespace V2Importer
{
    public class ImportCurrentUserService : ICurrentUserService
    {
        public string? UserId{ get; set; }
    }
}
