using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Rommelmarkten.Api.Common.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
