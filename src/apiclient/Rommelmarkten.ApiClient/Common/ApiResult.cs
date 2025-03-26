namespace Rommelmarkten.ApiClient.Common
{
    public class ApiResult<TSuccess, TError>
    {
        public ApiResult() : base()
        {
        }

        public bool Succeeded { get; set; }
        public TSuccess? Data { get; set; }
        public TError? Error { get; set; }

        //public HttpStatusCode StatusCode { get; }
    }
}
