using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Users.Commands.AuthenticateUser;
using Rommelmarkten.Api.Application.Users.Commands.ConfirmEmail;
using Rommelmarkten.Api.Application.Users.Commands.CreateUser;
using Rommelmarkten.Api.Application.Users.Commands.DeleteUser;
using Rommelmarkten.Api.Application.Users.Commands.UpdateAvatar;
using Rommelmarkten.Api.Application.Users.Commands.UpdateProfile;
using Rommelmarkten.Api.Application.Users.Queries.CreateUser;
using Rommelmarkten.Api.WebApi.Controllers;
using System.Net.Mime;

namespace Rommelmarkten.Api.WebApi.V1.Users
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CreateUserResult>> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(Create), result);
            }
            else
            {
                return BadRequest(result);
            }
            
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

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


        [HttpPut("profile")]
        public async Task<ActionResult> Update(UpdateProfileCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("avatar")]
        [Authorize] //prevent anonymous spammers
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK, "image/jpeg")]
        [RequestSizeLimit(5 * 1024 * 1024)] //5 MiB
        public async Task<ActionResult> Update(IFormFile avatarFile)
        {
            byte[] buffer = new byte[avatarFile.Length];
            using(var memoryStream = new MemoryStream())
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
