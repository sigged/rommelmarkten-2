using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Web.Controllers;
using Rommelmarkten.Api.Common.Web.Middlewares;
using Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Commands;
using Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Models;
using Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Requests;
using System.Net.Mime;

namespace Rommelmarkten.Api.Features.FAQs.Web.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class FAQItemsController : ApiControllerBase
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(CreateFAQItemCommand command)
        {
            var createdId = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), createdId);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(UpdateFAQItemCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteFAQItemCommand { Id = id });
            return NoContent();
        }

        [HttpGet]
        [OutputCache(Tags = [CacheTagNames.FAQ])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<FAQItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<FAQItemDto>>> GetPagedConfigurations([FromQuery] GetPagedFAQItemsRequest query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id:guid}")]
        [OutputCache(Tags = [CacheTagNames.FAQ])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<FAQItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FAQItemDto>> GetConfiguration(Guid id)
        {
            return await Mediator.Send(new GetFAQItemByIdRequest { Id = id });
        }
    }
}
