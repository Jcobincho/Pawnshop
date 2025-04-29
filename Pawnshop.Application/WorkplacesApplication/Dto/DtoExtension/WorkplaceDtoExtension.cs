using Pawnshop.Domain.Entities;

namespace Pawnshop.Application.WorkplacesApplication.Dto.DtoExtension
{
    public static class WorkplaceDtoExtension
    {
        public static WorkplaceDto WorkplaceParseToDto(this Workplace workplace)
        {
            return new WorkplaceDto
            {
                WorkplaceId = workplace.Id,
                Country = workplace.Country,
                Region = workplace.Region,
                StreetAndBuildingNumber = workplace.StreetAndBuildingNumber,
                ZipCode = workplace.ZipCode,
                City = workplace.City,
                CreatedAt = workplace.CreatedAt,
                CreatedBy = workplace.CreatedBy,
                EditedAt = workplace.EditedAt,
                EditedBy = workplace.EditedBy
            };
        }
    }
}
