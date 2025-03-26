using Rommelmarkten.ApiClient.Common;
using Rommelmarkten.ApiClient.Common.Payloads;
using Rommelmarkten.ApiClient.Config;
using System.Net.Http.Json;
using System.Text.Json;

namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ApiClientConfiguration configuration;

        public IHttpClientFactory HttpClientFactory => httpClientFactory;

        public UsersClient(IHttpClientFactory httpClientFactory, ApiClientConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<AuthenticationResult> Authenticate(LoginRequest loginRequest)
            => await PostAsJsonAsync<AuthenticationResult, LoginRequest>(loginRequest, "/api/v1/users/login");


        public Task<ApiResult<PaginatedList<UserProfileResult>, ProblemDetails>> GetPaged(PaginatedRequest pagedRequest)
        {
            return GetFromJsonAsync<PaginatedList<UserProfileResult>>($"/api/v1/users?pageNumber={pagedRequest.PageNumber}&pageSize={pagedRequest.PageSize}"); ;
        }


        public async Task<TResult> PostAsJsonAsync<TResult, TRequest>(TRequest request, string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.PostAsJsonAsync(endpoint, request);
            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result!;
        }

        public async Task<ApiResult<T, ProblemDetails>> GetFromJsonAsync<T>(string endpoint)
        {
            var haha = typeof(T);
            var kaka = haha.Name;
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.GetAsync(endpoint);

            if (response.Content.Headers.ContentLength > 0)
            {
                var content = await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<T>(content, ApiSerializerOptions.Default);
                    return new ApiResult<T, ProblemDetails>
                    {
                        Data = data,
                        Error = default,
                        Succeeded = true
                    };
                }
                else
                {
                    var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, ApiSerializerOptions.Default);
                    return new ApiResult<T, ProblemDetails>
                    {
                        Data = default,
                        Error = problemDetails,
                        Succeeded = false
                    };
                }
            }
            else
            { 
                //no content in body

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResult<T, ProblemDetails>
                    {
                        Data = default,
                        Error = null,
                        Succeeded = true
                    };
                }
                else
                {
                    return new ApiResult<T, ProblemDetails>
                    {
                        Data = default,
                        Error = new ProblemDetails
                        {
                            Title = response.ReasonPhrase,
                        },
                        Succeeded = true
                    };
                }
            }
        }
    }


}
