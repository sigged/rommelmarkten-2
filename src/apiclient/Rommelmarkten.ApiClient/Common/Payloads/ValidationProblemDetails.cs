namespace Rommelmarkten.ApiClient.Common.Payloads
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public IDictionary<string, ICollection<string>>? Errors { get; set; }

    }
}
