using System.Text.Json;
using System.Text;
using Pawnshop.Web.Exceptions;
using System.Net.Http.Headers;

namespace Pawnshop.Web.Services.ApiService
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string uri, Dictionary<string, string> queryParams = null, bool requireAuth = true)
        {
            return await SendAsync<T>(HttpMethod.Get, uri, requireAuth, queryParams);
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

        private async Task<T> SendAsync<T>(HttpMethod method, string uri, bool requireAuth, Dictionary<string, string> queryParams = null, object body = null)
        {
            var requestUri = BuildUriWithQuery(uri, queryParams);
            var request = new HttpRequestMessage(method, requestUri);

            if(requireAuth)
            {
                // var token = Token;
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

            try
            {
                using var doc = JsonDocument.Parse(errorContent);

                if (doc.RootElement.TryGetProperty("error", out var errorElement))
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
            catch (JsonException)
            {
                errorMessages.Add("Invalid error response format.");
            }

            if (!errorMessages.Any())
            {
                errorMessages.Add("Unknown error occurred.");
            }

            return errorMessages;
        }
    }
}
