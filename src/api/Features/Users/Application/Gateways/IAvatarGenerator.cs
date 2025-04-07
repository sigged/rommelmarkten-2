using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Domain.ValueObjects;

namespace Rommelmarkten.Api.Features.Users.Application.Gateways
{
    public interface IAvatarGenerator
    {
        Task<Blob> GenerateAvatar(IUser user, int size = 128);
    }
}