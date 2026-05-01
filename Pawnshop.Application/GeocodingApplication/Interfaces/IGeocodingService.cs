using Pawnshop.Application.GeocodingApplication.Dto;

namespace Pawnshop.Application.GeocodingApplication.Interfaces
{
    public interface IGeocodingService
    {
        Task<GeoCoordinates?> GeocodeAsync(
            string country,
            string region,
            string city,
            string streetAndBuildingNumber,
            string zipCode,
            CancellationToken cancellationToken);
    }
}
