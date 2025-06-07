using Pawnshop.Application.ClientsApplication.Responses;
using Pawnshop.Application.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ClientsApplication.Commands.AddClient
{
    public sealed class AddClientCommand : BaseCommand<AddClientResponse>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Birthday date is required.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "PESEL is required.")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "Number of ID card is required.")]
        public string IdCardNumber { get; set; }
        public string TelephoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
