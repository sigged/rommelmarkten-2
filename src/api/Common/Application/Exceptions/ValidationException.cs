using FluentValidation.Results;
using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Common.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }

        public static void ThrowWhenFailedResult(Result result)
        {
            if (!result.Succeeded)
            {
                var failures = result.Errors.Select(error =>
                new ValidationFailure
                {
                    PropertyName = string.Empty,
                    ErrorMessage = error
                });
                throw new ValidationException(failures);
            }
        }
    }
}