namespace Rommelmarkten.Api.Common.Application.Models
{
    public class CaptchaVerificationResultDto : Result
    {
        public CaptchaVerificationResultDto(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

    }
}