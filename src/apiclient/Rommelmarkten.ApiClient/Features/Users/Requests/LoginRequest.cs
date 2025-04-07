namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        //public async Task<PagedResult<UserResult>> GetAll(PagingRequest pagingRequest)
        //{
        //    var client = httpClientFactory.CreateClient(Constants.ClientName);
        //    var response = await client.GetAsync($"/api/v1/Users?PageNumber={pagingRequest.PageNumber}&PageSize={pagingRequest.PageSize}");
        //    response.EnsureSuccessStatusCode();
        //    var result = await response.Content.ReadFromJsonAsync<PagedResult<UserResult>>();
        //    return result ?? new PagedResult<UserResult>();
        //}

        public class LoginRequest
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }
    }
}
