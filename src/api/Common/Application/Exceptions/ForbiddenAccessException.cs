namespace Rommelmarkten.Api.Common.Application.Exceptions
{
    public class ForbiddenAccessException : ApplicationException
    {
        public ForbiddenAccessException() : base("Insufficient permissions")
        {
        }
        public ForbiddenAccessException(string message) : base(message) { }
    }
}
