using Pawnshop.Application.Base;
using Pawnshop.Application.WorkplacesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace
{
    public sealed class DeleteWorkplaceCommand : BaseCommand<DeleteWorkplaceResponse>
    {
        [Required(ErrorMessage = "Workplace id is required.")]
        public Guid WorkplaceId { get; set; }
    }
}
