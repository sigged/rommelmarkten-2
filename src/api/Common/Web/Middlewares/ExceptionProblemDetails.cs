using Microsoft.AspNetCore.Mvc;

namespace Rommelmarkten.Api.Common.Web.Middlewares
{
    public class ExceptionProblemDetails : ProblemDetails
    {
        public ExceptionProblemDetails(Exception exception)
        {
#if DEBUG
            Exception? currentException = exception;
            do
            {
                Detail = currentException.Message;
                Extensions["stackTrace"] = currentException.StackTrace;
                currentException = currentException.InnerException;
            }
            while (currentException != null);
#endif
        }
    }
}
