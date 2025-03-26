using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.AuthTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.RefreshToken
{
    public sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, JsonWebToken>
    {
        private readonly IUsersCommandService _usersCommandService;

        public RefreshTokenHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<JsonWebToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _usersCommandService.RefreshToken(request, cancellationToken);
        }
    }
}
