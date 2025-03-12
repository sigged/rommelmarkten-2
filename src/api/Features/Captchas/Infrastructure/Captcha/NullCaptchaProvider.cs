using Rommelmarkten.Api.Features.Captchas.Application.Gateways;
using Rommelmarkten.Api.Features.Captchas.Application.Models;

namespace Rommelmarkten.Api.Features.Captchas.Infrastructure.Captcha
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
