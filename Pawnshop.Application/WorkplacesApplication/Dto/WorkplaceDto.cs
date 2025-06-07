using Pawnshop.Application.Common.Base;

namespace Pawnshop.Application.WorkplacesApplication.Dto
{
    public class WorkplaceDto : BaseDto
    {
        public Guid WorkplaceId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string StreetAndBuildingNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
