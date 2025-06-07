using Pawnshop.Application.Common.Base;
using Pawnshop.Domain.AuthTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.RefreshToken
{
    public sealed class RefreshTokenCommand() : BaseCommand<JsonWebToken>
    {
        public string? RefreshToken { get; set; }
    }
}
