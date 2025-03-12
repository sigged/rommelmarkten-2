using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Common.Application.Interfaces
{
    public interface ICaptchaProvider
    {
        Task<string> GetChallenge();


        Task<CaptchaVerificationResultDto> VerifyChallenge(string captcha);
    }
}
