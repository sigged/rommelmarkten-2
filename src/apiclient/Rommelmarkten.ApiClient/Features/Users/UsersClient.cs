using Rommelmarkten.ApiClient.Config;
using System.Net.Http.Json;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

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
            => await PostAsJsonAsync<AuthenticationResult, LoginRequest>(loginRequest, "/api/v1/users/authenticate");


        public Task<AuthenticationResult> GetPaged(PaginatedRequest pagedRequest)
            => GetFromJsonAsync<AuthenticationResult>($"/api/v1/users?pageNumber={pagedRequest.PageNumber}&pageSize={pagedRequest.PageSize}");

        public async Task<TResult> PostAsJsonAsync<TResult, TRequest>(TRequest request, string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.PostAsJsonAsync(endpoint, request);
            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result!;
        }
        public async Task<TResult> GetFromJsonAsync<TResult>(string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var result = await client.GetFromJsonAsync<TResult>(endpoint);
            return result!;
        }
    }
}
