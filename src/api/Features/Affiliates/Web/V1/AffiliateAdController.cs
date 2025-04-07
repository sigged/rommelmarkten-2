using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Common.Web.Controllers;
using Rommelmarkten.Api.Common.Web.Middlewares;
using Rommelmarkten.Api.Features.Affiliates.Application.Commands;
using Rommelmarkten.Api.Features.Affiliates.Application.Models;
using Rommelmarkten.Api.Features.Affiliates.Application.Requests;
using System.Net.Mime;

namespace Rommelmarkten.Api.Features.Affiliates.Web.V1
{
    /// <summary>
    /// Controller for managing affiliate ads
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public class AffiliateAdsController : ApiControllerBase
    {
        /// <summary>
        /// Creates a new affiliate ad
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(CreateAffiliateAdCommand command)
        {
            var createdId = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), createdId);
        }

        /// <summary>
        /// Updates an existing affiliate ad
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(UpdateAffiliateAdCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing affiliate ad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteAffiliateAdCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Gets a paged list of affiliate ads
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(Tags = [CacheTagNames.AffiliateAd])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<AffiliateAdDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<AffiliateAdDto>>> GetPagedAffiliateAds([FromQuery] GetPagedAffiliateAdsRequest query)
        {
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Gets an affiliate ad by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [OutputCache(Tags = [CacheTagNames.AffiliateAd])]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginatedList<AffiliateAdDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AffiliateAdDto>> GetAffiliateAd(Guid id)
        {
            return await Mediator.Send(new GetAffiliateAdByIdRequest { Id = id });
        }
    }
}
