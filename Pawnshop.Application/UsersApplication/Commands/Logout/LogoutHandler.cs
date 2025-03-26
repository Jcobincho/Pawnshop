using MediatR;
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
        public async Task<LogoutResponse> Handle(LogoutCommand? request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
