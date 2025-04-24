using Pawnshop.Application.ClientsApplication.Commands.AddClient;
using Pawnshop.Application.ClientsApplication.Commands.DeleteClient;
using Pawnshop.Application.ClientsApplication.Commands.UpdateClient;

namespace Pawnshop.Application.ClientsApplication.Interfaces
{
    public interface IClientsCommandService
    {
        Task<Guid> AddClientAsync(AddClientCommand command, CancellationToken cancellationToken);
        Task UpdateClientAsync(UpdateClientCommand command, CancellationToken cancellationToken);
        Task DeleteClientAsync(DeleteClientCommand command, CancellationToken cancellationToken);
    }
}
