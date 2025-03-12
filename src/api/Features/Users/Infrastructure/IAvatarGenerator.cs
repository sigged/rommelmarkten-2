using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Domain.ValueObjects;

namespace Rommelmarkten.Api.Features.Affiliates.Infrastructure
{
    public interface IAvatarGenerator
    {
        Task<Blob> GenerateAvatar(IUser user, int size = 128);
    }
}