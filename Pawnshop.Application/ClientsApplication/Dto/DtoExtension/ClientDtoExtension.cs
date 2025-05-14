using Pawnshop.Domain.Entities;

namespace Pawnshop.Application.ClientsApplication.Dto.DtoExtension
{
    public static class ClientDtoExtension
    {
        public static ClientDto ClientParseToDto(this Client client)
        {
            return new ClientDto
            {
                ClientId = client.Id,
                Name = client.Name,
                SecondName = client.SecondName,
                Surname = client.Surname,
                BirthDate = client.BirthDate,
                Pesel = client.Pesel,
                IdCardNumber = client.IdCardNumber,
                TelephoneNumber = client.TelephoneNumber,
                Email = client.Email,
                Description = client.Description,
                CreatedAt = client.CreatedAt,
                CreatedBy = client.CreatedBy,
                EditedAt = client.EditedAt,
                EditedBy = client.EditedBy,
            };
        }
    }
}
