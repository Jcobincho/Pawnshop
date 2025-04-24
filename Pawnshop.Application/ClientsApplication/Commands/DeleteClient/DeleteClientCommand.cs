using Pawnshop.Application.Base;
using Pawnshop.Application.ClientsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ClientsApplication.Commands.DeleteClient
{
    public sealed class DeleteClientCommand : BaseCommand<DeleteClientResponse>
    {
        [Required(ErrorMessage = "Client id is required.")]
        public Guid ClientId { get; set; }
    }
}
