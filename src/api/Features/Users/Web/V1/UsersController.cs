using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Web.Controllers;
using Rommelmarkten.Api.Common.Web.Middlewares;
using Rommelmarkten.Api.Features.Users.Application.Commands.AuthenticateUser;
using Rommelmarkten.Api.Features.Users.Application.Commands.ConfirmEmail;
using Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser;
using Rommelmarkten.Api.Features.Users.Application.Commands.DeleteUser;
using Rommelmarkten.Api.Features.Users.Application.Commands.ExchangeRefreshToken;
using Rommelmarkten.Api.Features.Users.Application.Commands.ForgotPassword;
using Rommelmarkten.Api.Features.Users.Application.Commands.ResendConfirmationEmail;
using Rommelmarkten.Api.Features.Users.Application.Commands.ResetPassword;
using Rommelmarkten.Api.Features.Users.Application.Commands.UpdateAvatar;
using Rommelmarkten.Api.Features.Users.Application.Commands.UpdateProfile;
using Rommelmarkten.Api.Features.Users.Application.Models;
using Rommelmarkten.Api.Features.Users.Application.Queries;
using System.Net.Mime;

namespace Rommelmarkten.Api.Features.Users.Web.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class UsersController : ApiControllerBase
    {

        [HttpGet]
        [OutputCache(Tags = [CacheTagNames.Users])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<UserProfileDto>>> GetPagedProfiles([FromQuery] GetPagedProfilesRequest query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("current")]
        [OutputCache(Tags = [CacheTagNames.Users])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCurrentUserProfile()
        {
            return Ok(await Mediator.Send(new GetCurrentUserProfileQuery()));
        }


        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Register(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Register), result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AccessTokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Authenticate(AuthenticateUserCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);
            else
                return Unauthorized(result);
        }

        [HttpPost("exchange-refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AccessTokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> RefreshAccessToken(ExchangeRefreshTokenCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);
            else
                return Unauthorized(result);
        }

        [HttpGet("get-email-confirm-token")]
        [ProducesResponseType(typeof(TokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GenerateEmailConfirmationToken(string userId)
        {
            var command = new GenerateEmailConfirmationTokenCommand { 
                UserId = userId 
            };
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            var result = await Mediator.Send(command);

            //if (!result.Succeeded)
            //    return BadRequest(result);

            return NoContent();
        }

        [HttpPost("resend-confirmation-email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ConfirmEmail(ResendConfirmationEmailCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            await Mediator.Send(command); //todo: prevent exposing notfoundexception!
            return NoContent();
        }

        [HttpGet("get-password-reset-token")]
        [ProducesResponseType(typeof(TokenResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GeneratePasswordResetToken(GeneratePasswordResetCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ForgotPassword(ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }


        //[HttpPost("manage-2fa")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        private async Task<ActionResult> ManageTwoFactorAuthentication(ManageTwoFactorAuthenticationCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("profile")]
        public async Task<ActionResult> Update(UpdateProfileCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(DeleteUserCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return NoContent();
        }

        [HttpPut("avatar")]
        [Authorize] //prevent anonymous spammers
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK, "image/jpeg")]
        [RequestSizeLimit(5 * 1024 * 1024)] //5 MiB
        public async Task<ActionResult> Update(IFormFile avatarFile)
        {
            byte[] buffer = new byte[avatarFile.Length];
            using (var memoryStream = new MemoryStream())
            {
                await avatarFile.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await memoryStream.WriteAsync(buffer, 0, buffer.Length);
            }

            var command = new UpdateAvatarCommand
            {
                Avatar = new BlobDto
                {
                    Content = buffer,
                    Name = avatarFile.FileName,
                    Type = avatarFile.ContentType
                }
            };
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("avatar")]
        public async Task<ActionResult> Delete()
        {
            await Mediator.Send(new UpdateAvatarCommand { Avatar = null });

            return NoContent();
        }
    }
}
