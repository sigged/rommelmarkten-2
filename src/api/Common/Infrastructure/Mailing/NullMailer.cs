using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Infrastructure.Mailing
{
    public class NullMailer : IMailer
    {
        public Task SendEmailConfirmationLink(IUser user, string emailConfirmationLink)
        {
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLink(IUser user, string passwordLink)
        {
            return Task.CompletedTask;
        }
    }
}
