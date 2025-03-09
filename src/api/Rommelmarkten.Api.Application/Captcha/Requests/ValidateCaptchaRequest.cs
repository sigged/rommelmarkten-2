using MediatR;
using Rommelmarkten.Api.Application.Captcha.Models;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.Captcha.Requests
{
    public class ValidateCaptchaRequest : IRequest<CaptchaVerificationResultDto>
    {
        public required string Captcha { get; set; }
    }
    public class ValidateCaptchaRequestHandler : IRequestHandler<ValidateCaptchaRequest, CaptchaVerificationResultDto>
    {
        private readonly ICaptchaProvider captchaProvider;

        public ValidateCaptchaRequestHandler(ICaptchaProvider captchaProvider)
        {
            this.captchaProvider = captchaProvider;
        }

        public async Task<CaptchaVerificationResultDto> Handle(ValidateCaptchaRequest request, CancellationToken cancellationToken)
        {
            var result = await captchaProvider.VerifyChallenge(request.Captcha);
            return result;
        }
    }
}
