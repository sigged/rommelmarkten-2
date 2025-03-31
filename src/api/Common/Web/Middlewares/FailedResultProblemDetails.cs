using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Rommelmarkten.Api.Common.Web.Middlewares
{
    //todo: decide if we want to use this class for 'Result.Succeeded = false' handling (replacing ThrowWhenFailedResult)
    public class FailedResultProblemDetails : ProblemDetails
    {

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationProblemDetails"/> using the specified <paramref name="errors"/>.
        /// </summary>
        /// <param name="errors">The failed result errors.</param>
        public FailedResultProblemDetails(IDictionary<string, string[]> errors)
        {
            Errors = new Dictionary<string, string[]>(errors ?? throw new ArgumentNullException(nameof(errors)), StringComparer.Ordinal)
        }

        /// <summary>
        /// Gets the validation errors associated with this instance of <see cref="HttpValidationProblemDetails"/>.
        /// </summary>
        [JsonPropertyName("errors")]
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
    }
}
