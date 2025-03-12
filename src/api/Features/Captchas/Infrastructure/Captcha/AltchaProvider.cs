using Ixnas.AltchaNet;
using Rommelmarkten.Api.Features.Captchas.Application.Gateways;
using Rommelmarkten.Api.Features.Captchas.Application.Models;

namespace Rommelmarkten.Api.Features.Captchas.Infrastructure.Captcha
{
    public class AltchaProvider : ICaptchaProvider
    {
        private readonly AltchaService selfHostedService;

        public AltchaProvider(AltchaService selfHostedService)
        {
            this.selfHostedService = selfHostedService;
        }

        public Task<string> GetChallenge()
        {
            var challenge = selfHostedService.Generate();
            return Task.FromResult(challenge.Challenge);
        }

        public async Task<CaptchaVerificationResultDto> VerifyChallenge(string captcha)
        {
            var result = await selfHostedService.Validate(captcha);
            if (result.IsValid)
            {
                return new CaptchaVerificationResultDto(true, []);
            }
            else
            {
                return new CaptchaVerificationResultDto(false, [result.ValidationError.Message]);
            }

        }
    }
}
