using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.DeleteUser
{
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUsersCommandService _usersCommandService;

        public DeleteUserHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _usersCommandService.DeleteUserAsync(request.UserId, cancellationToken);

            return new DeleteUserResponse();
        }
    }
}
