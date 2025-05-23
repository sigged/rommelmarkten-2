﻿using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Captcha.Models
{
    public class CaptchaVerificationResultDto : Result
    {
        public CaptchaVerificationResultDto(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

    }
}