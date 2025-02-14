using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Domain.ValueObjects;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IAvatarGenerator
    {
        Task<Blob> GenerateAvatar(IUser user, int size = 128);
    }
}