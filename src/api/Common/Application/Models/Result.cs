namespace Rommelmarkten.Api.Common.Application.Models
{
    public class Result : ResultBase<Result, string> 
    {
        public Result() : base()
        {
        }

        public Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
    }
}
