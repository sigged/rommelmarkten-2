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

        public Task<ApiResult<AuthenticationResult, ProblemDetails>> Authenticate(LoginRequest loginRequest)
            => PostAsJsonAsync<AuthenticationResult, LoginRequest>(loginRequest, $"/api/v1/users/login");

        public Task<ApiResult<PaginatedList<UserProfileResult>, ProblemDetails>> GetPaged(PaginatedRequest pagedRequest)
            => GetFromJsonAsync<PaginatedList<UserProfileResult>>($"/api/v1/users?pageNumber={pagedRequest.PageNumber}&pageSize={pagedRequest.PageSize}");

        public Task<ApiResult<RegisteredResult, ProblemDetails>> Register(RegisterUserRequest registerRequest)
            => PostAsJsonAsync<RegisteredResult, RegisterUserRequest>(registerRequest, $"/api/v1/users/register");

        public Task<ApiResult<EmailConfirmTokenResult, ProblemDetails>> GetEmailConfirmationToken(string userId)
            => GetFromJsonAsync<EmailConfirmTokenResult>($"/api/v1/users/get-email-confirm-token?userId={userId}");

        public Task<ApiResult<EmptyResult, ProblemDetails>> ResendConfirmationEmail(ResendConfirmationEmailCommand resendConfirmationEmailCommand)
            => PostAsJsonAsync<EmptyResult, ResendConfirmationEmailCommand>(resendConfirmationEmailCommand, $"/api/v1/users/resend-confirmation-email");

        public Task<ApiResult<EmptyResult, ProblemDetails>> ConfirmEmailToken(ConfirmEmailCommand confirmEmail)
            => PostAsJsonAsync<EmptyResult, ConfirmEmailCommand>(confirmEmail, $"/api/v1/users/confirm-email");

        public Task<ApiResult<EmptyResult, ProblemDetails>> ForgotPassword(ForgotPasswordCommand forgotPasswordCommand)
            => PostAsJsonAsync<EmptyResult, ForgotPasswordCommand>(forgotPasswordCommand, $"/api/v1/users/forgot-password");

        public Task<ApiResult<PasswordResetTokenResult, ProblemDetails>> GetPasswordResetToken(string userId)
            => GetFromJsonAsync<PasswordResetTokenResult>($"/api/v1/users/get-password-reset-token?userId={userId}");

        public Task<ApiResult<EmptyResult, ProblemDetails>> ResetPassword(ResetPasswordCommand resetPasswordCommand)
            => PostAsJsonAsync<EmptyResult, ResetPasswordCommand>(resetPasswordCommand, $"/api/v1/users/reset-password");

        public Task<ApiResult<UserProfileResult, ProblemDetails>> GetCurrentUser()
            => GetFromJsonAsync<UserProfileResult>($"/api/v1/users/current");

        public Task<ApiResult<EmptyResult, ProblemDetails>> DeleteUser(string userId)
            => DeleteFromJsonAsync<EmptyResult>($"/api/v1/users/{userId}");


        public async Task<ApiResult<TResult, ProblemDetails>> GetFromJsonAsync<TResult>(string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.GetAsync(endpoint);

            return await ReadResponseBody<TResult>(response);
        }

        public async Task<ApiResult<TResult, ProblemDetails>> PostAsJsonAsync<TResult, TRequest>(TRequest request, string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.PostAsJsonAsync(endpoint, request);
            return await ReadResponseBody<TResult>(response);
        }

        public async Task<ApiResult<TResult, ProblemDetails>> DeleteFromJsonAsync<TResult>(string endpoint)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.DeleteAsync(endpoint);
            return await ReadResponseBody<TResult>(response);
        }

        private async Task<ApiResult<TResult, ProblemDetails>> ReadResponseBody<TResult>(HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentLength > 0)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<TResult>(content, ApiSerializerOptions.Default);
                    return new ApiResult<TResult, ProblemDetails>
                    {
                        Data = data!,
                        Error = default!,
                        Succeeded = true
                    };
                }
                else
                {
                    ProblemDetails problemDetails = default!;

                    if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableContent)
                    {
                        problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, ApiSerializerOptions.Default)!;
                    }
                    else {
                        problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, ApiSerializerOptions.Default)!;
                    }

                    problemDetails.Title = response.ReasonPhrase;
                    problemDetails.Status = (int)response.StatusCode;
                    return new ApiResult<TResult, ProblemDetails>
                    {
                        Data = default!,
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
                    return new ApiResult<TResult, ProblemDetails>
                    {
                        Data = default!,
                        Error = null!,
                        Succeeded = true
                    };
                }
                else
                {
                    return new ApiResult<TResult, ProblemDetails>
                    {
                        Data = default!,
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
