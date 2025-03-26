namespace Rommelmarkten.ApiClient.Common
{
    public class ApiResult<TSuccess, TError>
    {
        public ApiResult() : base()
        {
        }

        public bool Succeeded { get; set; }
        public TSuccess Data { get; set; } = default!;
        public TError Error { get; set; } = default!;

        //public HttpStatusCode StatusCode { get; }
    }
}
