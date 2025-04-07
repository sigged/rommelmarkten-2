using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rommelmarkten.Api.Common.Web.Controllers;
using Rommelmarkten.Api.Features.Captchas.Application.Models;
using Rommelmarkten.Api.Features.Captchas.Application.Requests;
using System.Net.Mime;

namespace Rommelmarkten.Api.Features.Captchas.Web.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class CaptchaController : ApiControllerBase
    {
        [HttpGet("challenge")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CaptchaChallengeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CaptchaChallengeResponseDto>> RequestChallenge()
        {
            var response = await Mediator.Send(new CaptchaChallengeRequest());
            return response;
        }

        [HttpPost("verify")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CaptchaVerificationResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CaptchaVerificationResultDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CaptchaVerificationResultDto>> RequestVerification(ValidateCaptchaRequest request)
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
