using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Application.Interfaces
{
    public interface IMailer
    {
        Task SendPasswordResetLink(IUser user, string passwordLink);

        Task SendEmailConfirmationLink(IUser user, string emailConfirmationLink);

    }
}
