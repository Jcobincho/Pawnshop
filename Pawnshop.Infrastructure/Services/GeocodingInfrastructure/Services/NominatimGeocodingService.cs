using Microsoft.Extensions.Configuration;
using Pawnshop.Application.GeocodingApplication.Dto;
using Pawnshop.Application.GeocodingApplication.Interfaces;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pawnshop.Infrastructure.Services.GeocodingInfrastructure.Services
{
    internal sealed class NominatimGeocodingService : IGeocodingService
    {
        private static readonly HttpClient HttpClient = new()
        {
            BaseAddress = new Uri("https://nominatim.openstreetmap.org/")
        };

        private static readonly SemaphoreSlim RequestLock = new(1, 1);
        private static DateTimeOffset _lastRequestAt = DateTimeOffset.MinValue;

        private readonly string _userAgent;

        public NominatimGeocodingService(IConfiguration configuration)
        {
            _userAgent = configuration["Nominatim:UserAgent"] ?? "PawnshopStudentProject/1.0";
        }

        public async Task<GeoCoordinates?> GeocodeAsync(
            string country,
            string region,
            string city,
            string streetAndBuildingNumber,
            string zipCode,
            CancellationToken cancellationToken)
        {
            var queries = BuildAddressQueries(country, region, city, streetAndBuildingNumber, zipCode);
            if (!queries.Any())
            {
                return null;
            }

            await RequestLock.WaitAsync(cancellationToken);
            try
            {
                foreach (var query in queries)
                {
                    var elapsed = DateTimeOffset.UtcNow - _lastRequestAt;
                    if (elapsed < TimeSpan.FromSeconds(1))
                    {
                        await Task.Delay(TimeSpan.FromSeconds(1) - elapsed, cancellationToken);
                    }

                    using var request = new HttpRequestMessage(
                        HttpMethod.Get,
                        $"search?format=jsonv2&limit=1&q={Uri.EscapeDataString(query)}");

                    request.Headers.UserAgent.ParseAdd(_userAgent);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using var response = await HttpClient.SendAsync(request, cancellationToken);
                    _lastRequestAt = DateTimeOffset.UtcNow;

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }

                    await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                    var results = await JsonSerializer.DeserializeAsync<List<NominatimSearchResult>>(contentStream, cancellationToken: cancellationToken);
                    var firstResult = results?.FirstOrDefault();

                    if (firstResult == null)
                    {
                        continue;
                    }

                    if (double.TryParse(firstResult.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var latitude)
                        && double.TryParse(firstResult.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var longitude))
                    {
                        return new GeoCoordinates(latitude, longitude);
                    }
                }

                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (JsonException)
            {
                return null;
            }
            finally
            {
                RequestLock.Release();
            }
        }

        private static List<string> BuildAddressQueries(
            string country,
            string region,
            string city,
            string streetAndBuildingNumber,
            string zipCode)
        {
            var queries = new[]
            {
                JoinAddress(streetAndBuildingNumber, zipCode, city, region, country),
                JoinAddress(streetAndBuildingNumber, zipCode, city, country),
                JoinAddress(streetAndBuildingNumber, city, country),
                JoinAddress(zipCode, city, country),
                JoinAddress(city, country)
            };

            return queries
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();
        }

        private static string JoinAddress(params string[] parts)
        {
            return string.Join(", ", parts.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        private sealed class NominatimSearchResult
        {
            [JsonPropertyName("lat")]
            public string? Latitude { get; set; }

            [JsonPropertyName("lon")]
            public string? Longitude { get; set; }
        }
    }
}
