﻿using Microsoft.AspNetCore.Identity;
using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}