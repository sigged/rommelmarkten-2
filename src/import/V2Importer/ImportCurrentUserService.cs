using Rommelmarkten.Api.Common.Application.Interfaces;

namespace V2Importer
{
    public class ImportCurrentUserService : ICurrentUserService
    {
        public string? UserId{ get; set; }
    }
    public class AlexAdminCurrentUserService : ICurrentUserService
    {
        public string? UserId { get; set; } = "4867bf1f-6865-4065-81fe-ebf1a575beac";
    }
}
