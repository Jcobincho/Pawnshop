using Pawnshop.Application.ClientsApplication.Commands.AddClient;

namespace Pawnshop.Application.ClientsApplication.Interfaces
{
    public interface IClientsCommandService
    {
        Task<Guid> AddClientAsync(AddClientCommand command, CancellationToken cancellationToken);
        Task 
    }
}
