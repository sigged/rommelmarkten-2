﻿using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Users.Models
{
    public class TokenResult : Result
    {
        public TokenResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public required string Token { get; set; }
    }

}
