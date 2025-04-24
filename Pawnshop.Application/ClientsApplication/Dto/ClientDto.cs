using Pawnshop.Application.Base;

namespace Pawnshop.Application.ClientsApplication.Dto
{
    public class ClientDto : BaseDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Pesel { get; set; }
        public string IdCardNumber { get; set; }
        public string TelephoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
