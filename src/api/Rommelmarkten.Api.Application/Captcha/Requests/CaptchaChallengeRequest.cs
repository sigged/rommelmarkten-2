using MediatR;
using Rommelmarkten.Api.Application.Captcha.Models;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.Captcha.Requests
{
    public class CaptchaChallengeRequest : IRequest<CaptchaChallengeResponseDto>
    {

    }

    public class ChallengeRequestHandler : IRequestHandler<CaptchaChallengeRequest, CaptchaChallengeResponseDto>
    {
        private readonly ICaptchaProvider captchaProvider;

        public ChallengeRequestHandler(ICaptchaProvider captchaProvider)
        {
            this.captchaProvider = captchaProvider;
        }

        public async Task<CaptchaChallengeResponseDto> Handle(CaptchaChallengeRequest request, CancellationToken cancellationToken)
        {
            var challenge = await captchaProvider.GetChallenge();
            return new CaptchaChallengeResponseDto
            {
                Challenge = challenge
            };
        }
    }

}
