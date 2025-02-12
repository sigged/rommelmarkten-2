using Asp.Versioning;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Users.Commands.AuthenticateUser;
using Rommelmarkten.Api.Application.Users.Commands.CreateUser;
using Rommelmarkten.Api.Application.Users.Commands.UpdateAvatar;
using Rommelmarkten.Api.Application.Users.Commands.UpdateProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rommelmarkten.Api.WebApi.Controllers;
using Rommelmarkten.Api.WebApi.Middlewares;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.WebApi.V1.Users
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ErrorResponse))]
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<string>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Authenticate(AuthenticateUserCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);
            else
                return Unauthorized(result);
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
