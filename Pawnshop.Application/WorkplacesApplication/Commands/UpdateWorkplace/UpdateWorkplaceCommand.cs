using Pawnshop.Application.Base;
using Pawnshop.Application.WorkplacesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace
{
    public sealed class UpdateWorkplaceCommand : BaseCommand<UpdateWorkplaceResponse>
    {
        [Required(ErrorMessage = "Workplace id is required.")]
        public Guid WorkplaceId { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Street/building number is required.")]
        public string StreetAndBuildingNumber { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}
