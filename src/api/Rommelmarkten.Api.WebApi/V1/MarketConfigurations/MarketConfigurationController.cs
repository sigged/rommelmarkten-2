using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rommelmarkten.Api.Application.MarketConfigurations.Commands;
using Rommelmarkten.Api.WebApi.Controllers;
using Rommelmarkten.Api.WebApi.Middlewares;
using System.Net.Mime;

namespace Rommelmarkten.Api.WebApi.V1.Users
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class MarketConfigurationController : ApiControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(CreateMarketConfigurationCommand command)
        {
            await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), "This should be the Id");
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateMarketConfigurationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
