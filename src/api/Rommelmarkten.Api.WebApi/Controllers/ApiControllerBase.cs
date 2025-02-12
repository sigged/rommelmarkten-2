using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Rommelmarkten.Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
