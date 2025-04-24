using MediatR;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ClientsApplication.Responses;

namespace Pawnshop.Application.ClientsApplication.Commands.DeleteClient
{
    public sealed class DeleteClientHandler : IRequestHandler<DeleteClientCommand, DeleteClientResponse>
    {
        private readonly IClientsCommandService _clientsCommandService;

        public DeleteClientHandler(IClientsCommandService clientsCommandService)
        {
            _clientsCommandService = clientsCommandService;
        }

        public async Task<DeleteClientResponse> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            await _clientsCommandService.DeleteClientAsync(request, cancellationToken);

            return new DeleteClientResponse();
        }
    }
}
