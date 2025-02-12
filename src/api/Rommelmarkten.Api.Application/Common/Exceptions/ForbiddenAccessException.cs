namespace Rommelmarkten.Api.Application.Common.Exceptions
{
    public class ForbiddenAccessException : ApplicationException
    {
        public ForbiddenAccessException() : base("Insufficient permissions")
        {
        }
        public ForbiddenAccessException(string message) : base(message) { }
    }
}
