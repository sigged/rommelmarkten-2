using Rommelmarkten.Api.Application.Captcha.Models;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Infrastructure.Security.Captcha
{
    public class NullCaptchaProvider : ICaptchaProvider
    {
        public Task<string> GetChallenge()
        {
            return Task.FromResult("FAKE CHALLENGE");
        }

        public Task<CaptchaVerificationResultDto> VerifyChallenge(string captcha)
        {
            return Task.FromResult(new CaptchaVerificationResultDto(true, []));
        }
    }
}
