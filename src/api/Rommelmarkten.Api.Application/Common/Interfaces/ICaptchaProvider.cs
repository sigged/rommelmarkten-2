using Rommelmarkten.Api.Application.Captcha.Models;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface ICaptchaProvider
    {
        Task<string> GetChallenge();


        Task<CaptchaVerificationResultDto> VerifyChallenge(string captcha);
    }
}
