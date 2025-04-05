using System.Text.Json;
using System.Text;
using Pawnshop.Web.Exceptions;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using Pawnshop.Domain.AuthTokens;
using Pawnshop.Web.Services.AuthenticationService;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Responses;
using MediatR;

namespace Pawnshop.Web.Services.ApiService
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authStateProvider;

        public ApiService(HttpClient httpClient, ProtectedLocalStorage localStorage, NavigationManager navigationManager, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri, Dictionary<string, string> queryParams = null, bool requireAuth = true)
        {
            return await SendAsync<TResponse>(HttpMethod.Get, uri, requireAuth, queryParams);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest body, bool requireAuth = true)
        {
            return await SendAsync<TResponse>(HttpMethod.Post, uri, requireAuth, null, body);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest body, bool requireAuth = true)
        {
            return await SendAsync<TResponse>(HttpMethod.Put, uri, requireAuth, null, body);
        }

        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string uri, TRequest body, bool requireAuth = true)
        {
            return await SendAsync<TResponse>(HttpMethod.Delete, uri, requireAuth, null, body);
        }

        public async Task LogoutHandler()
        {
            var sessionState = (await _localStorage.GetAsync<JsonWebToken>("sessionState")).Value;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/Users/logout");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.AccessToken);
                var jsonContent = JsonSerializer.Serialize(new LogoutCommand() { RefreshToken = sessionState.RefreshToken.Token });
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(request);
            }
            catch (Exception ex) { }
            finally
            {
                await ((AuthStateProviderService)_authStateProvider).MarkUserAsLoggedOut();
                _navigationManager.NavigateTo("/");
            }
        }

        private async Task SetAuthorizeHeader(HttpRequestMessage request)
        {
            try
            {
                var sessionState = (await _localStorage.GetAsync<JsonWebToken>("sessionState")).Value;
                if (sessionState != null && !string.IsNullOrEmpty(sessionState.AccessToken))
                {
                    if (sessionState.Expires < DateTimeOffset.UtcNow.ToUnixTimeSeconds() && sessionState.RefreshToken.Expires < DateTime.UtcNow)
                    {
                        await LogoutHandler();
                    }
                    else if(sessionState.Expires > DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.AccessToken);
                    }
                    else if (sessionState.Expires < DateTimeOffset.UtcNow.ToUnixTimeSeconds() && sessionState.RefreshToken.Expires > DateTime.UtcNow)
                    {
                        try
                        {
                            var res = await PostAsync<RefreshTokenCommand,JsonWebToken>("/Users/refresh-token", new RefreshTokenCommand() { RefreshToken = sessionState.RefreshToken.Token });

                            await ((AuthStateProviderService)_authStateProvider).MarkUserAsAuthenticated(res);
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", res.AccessToken);
                        }
                        catch(ApiException)
                        {
                            await LogoutHandler();
                        }
                    }
                    
                }
                else
                {
                    _navigationManager.NavigateTo("/");
                }
            }
            catch (Exception ex)
            {
                _navigationManager.NavigateTo("/");
            }
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string uri, bool requireAuth, Dictionary<string, string> queryParams = null, object body = null)
        {
            var requestUri = BuildUriWithQuery(uri, queryParams);
            var request = new HttpRequestMessage(method, requestUri);

            if(requireAuth)
            {
                await SetAuthorizeHeader(request);
            }

            if (body != null)
            {
                var jsonContent = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = ParseErrorMessages(errorContent);
                throw new ApiException(errorMessages, response.StatusCode);
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private string BuildUriWithQuery(string uri, Dictionary<string, string> queryParams)
        {
            if (queryParams == null || !queryParams.Any())
                return uri;

            var queryString = string.Join("&", queryParams
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

            return $"{uri}?{queryString}";
        }

        private List<string> ParseErrorMessages(string errorContent)
        {
            var errorMessages = new List<string>();

            AddErrorToList(errorContent, errorMessages, "errors");

            if (!errorMessages.Any())
            {
                AddErrorToList(errorContent, errorMessages, "error");
            }

            if (!errorMessages.Any())
            {
                errorMessages.Add("Unknown error occurred.");
            }

            return errorMessages;
        }

        private void AddErrorToList(string errorContent, List<string> errorMessages, string tryGetProperty)
        {
            try
            {
                using var doc = JsonDocument.Parse(errorContent);

                if (doc.RootElement.TryGetProperty(tryGetProperty, out var errorElement))
                {
                    if (errorElement.ValueKind == JsonValueKind.String)
                    {
                        errorMessages.Add(errorElement.GetString()!);
                    }
                    else if (errorElement.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var property in errorElement.EnumerateObject())
                        {
                            if (property.Value.ValueKind == JsonValueKind.Array)
                            {
                                errorMessages.AddRange(property.Value.EnumerateArray()
                                             .Select(x => x.GetString()!)
                                );
                            }
                        }
                    }
                }
            }
            catch(JsonException)
            {
                errorMessages.Add("Invalid error response format.");
            }
        }
    }
}
