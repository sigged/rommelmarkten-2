using MediatR;
using Microsoft.Extensions.Logging;
using Rommelmarkten.Api.Common.Application.Exceptions;

namespace Rommelmarkten.Api.Common.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex) when (ex is not ValidationException)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "Rommelmarkten.API Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
}
