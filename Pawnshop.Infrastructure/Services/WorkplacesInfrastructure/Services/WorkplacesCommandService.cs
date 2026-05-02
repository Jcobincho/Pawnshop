using Pawnshop.Application.GeocodingApplication.Dto;
using Pawnshop.Application.GeocodingApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services
{
    internal sealed class WorkplacesCommandService : IWorkplacesCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IWorkplacesQueryService _workplacesQueryService;
        private readonly IGeocodingService _geocodingService;

        public WorkplacesCommandService(
            DbContext dbContext,
            IWorkplacesQueryService workplacesQueryService,
            IGeocodingService geocodingService)
        {
            _dbContext = dbContext;
            _workplacesQueryService = workplacesQueryService;
            _geocodingService = geocodingService;
        }

        public async Task<Guid> AddWorkplaceAsync(AddWorkplaceCommand command, CancellationToken cancellationToken)
        {
            var coordinates = await TryResolveCoordinatesAsync(
                command.Country,
                command.Region,
                command.City,
                command.StreetAndBuildingNumber,
                command.ZipCode,
                command.Latitude,
                command.Longitude,
                cancellationToken);

            Workplace newWorkplace = new Workplace()
            {
                Country = command.Country,
                Region = command.Region,
                StreetAndBuildingNumber = command.StreetAndBuildingNumber,
                ZipCode = command.ZipCode,
                City = command.City,
                Latitude = coordinates?.Latitude,
                Longitude = coordinates?.Longitude,
            };

            await _dbContext.Workplaces.AddAsync(newWorkplace, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newWorkplace.Id;
        }

        public async Task UpdateWorkplaceAsync(UpdateWorkplaceCommand command, CancellationToken cancellationToken)
        {
            var workplace = await _workplacesQueryService.GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);
            var addressChanged = workplace.Country != command.Country
                || workplace.Region != command.Region
                || workplace.City != command.City
                || workplace.StreetAndBuildingNumber != command.StreetAndBuildingNumber
                || workplace.ZipCode != command.ZipCode;

            var coordinates = command.Latitude.HasValue && command.Longitude.HasValue
                ? new GeoCoordinates(command.Latitude.Value, command.Longitude.Value)
                : addressChanged || !workplace.Latitude.HasValue || !workplace.Longitude.HasValue
                    ? await TryResolveCoordinatesAsync(
                        command.Country,
                        command.Region,
                        command.City,
                        command.StreetAndBuildingNumber,
                        command.ZipCode,
                        null,
                        null,
                        cancellationToken)
                    : new GeoCoordinates(workplace.Latitude.Value, workplace.Longitude.Value);

            workplace.Country = command.Country;
            workplace.Region = command.Region;
            workplace.StreetAndBuildingNumber = command.StreetAndBuildingNumber;
            workplace.ZipCode = command.ZipCode;
            workplace.City = command.City;
            workplace.Latitude = coordinates?.Latitude;
            workplace.Longitude = coordinates?.Longitude;

            _dbContext.Workplaces.Update(workplace);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteWorkplaceAsync(DeleteWorkplaceCommand command, CancellationToken cancellationToken)
        {
            var workplace = await _workplacesQueryService.GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);

            _dbContext.Workplaces.Remove(workplace);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<GeoCoordinates?> TryResolveCoordinatesAsync(
            string country,
            string region,
            string city,
            string streetAndBuildingNumber,
            string zipCode,
            double? latitude,
            double? longitude,
            CancellationToken cancellationToken)
        {
            if (latitude.HasValue && longitude.HasValue)
            {
                return new GeoCoordinates(latitude.Value, longitude.Value);
            }

            var coordinates = await _geocodingService.GeocodeAsync(
                country,
                region,
                city,
                streetAndBuildingNumber,
                zipCode,
                cancellationToken);

            return coordinates;
        }
    }
}
