namespace Rommelmarkten.Api.Common.Application.Models
{
    public abstract class ResultBase<TResult, TError>
        where TResult : ResultBase<TResult, TError>, new()
    {

        protected ResultBase()
        {
            Succeeded = false;
            Errors = [];
        }

        public ResultBase(bool succeeded, IEnumerable<TError> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; internal set; }

        public TError[] Errors { get; internal set; }

        public static TResult Success()
        {
            return new TResult()
            {
                Succeeded = true,
                Errors = []
            };
        }

        public static TResult Failure(IEnumerable<TError> errors)
        {
            return new TResult()
            {
                Succeeded = true,
                Errors = errors.ToArray()
            };
        }
    }
}
