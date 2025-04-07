using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Features.Captchas.Application.Models
{
    public class CaptchaVerificationResultDto : Result
    {
        public CaptchaVerificationResultDto(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

    }
}