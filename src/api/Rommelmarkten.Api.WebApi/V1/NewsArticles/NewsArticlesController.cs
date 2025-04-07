using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.NewsArticles.Commands.Create;
using Rommelmarkten.Api.Application.NewsArticles.Commands.Delete;
using Rommelmarkten.Api.Application.NewsArticles.Commands.Update;
using Rommelmarkten.Api.Application.NewsArticles.Models;
using Rommelmarkten.Api.Application.NewsArticles.Requests;
using Rommelmarkten.Api.WebApi.Controllers;
using Rommelmarkten.Api.WebApi.Middlewares;
using System.Net.Mime;

namespace Rommelmarkten.Api.WebApi.V1.NewsArticles
{
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class NewsArticlesController : ApiControllerBase
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(CreateNewsArticleCommand command)
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
        public async Task<ActionResult> Update(UpdateNewsArticleCommand command)
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
            await Mediator.Send(new DeleteNewsArticleCommand { Id = id });
            return NoContent();
        }

        [HttpGet]
        [OutputCache(Tags = [CacheTagNames.NewsArticle])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<NewsArticleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<NewsArticleDto>>> GetPagedConfigurations([FromQuery] GetPagedNewsArticlesRequest query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id:guid}")]
        [OutputCache(Tags = [CacheTagNames.NewsArticle])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<NewsArticleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<NewsArticleDto>> GetConfiguration(Guid id)
        {
            return await Mediator.Send(new GetNewsArticleByIdRequest { Id = id });
        }
    }
}
