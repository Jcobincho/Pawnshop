using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.Logout
{
    public sealed class LogoutHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        private readonly IUsersCommandService _usersCommandService;

        public LogoutHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<LogoutResponse> Handle(LogoutCommand? request, CancellationToken cancellationToken)
        {
            await _usersCommandService.LogoutAsync(request, cancellationToken);

            var x = request.UserIdFromClaims;

            return new LogoutResponse() { Response = "You have been successfully logged out." };
        }
    }
}
