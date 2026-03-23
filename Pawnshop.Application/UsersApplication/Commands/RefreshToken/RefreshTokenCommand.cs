using Pawnshop.Application.Common.Base;
using Pawnshop.Domain.AuthTokens;

namespace Pawnshop.Application.UsersApplication.Commands.RefreshToken
{
    public sealed class RefreshTokenCommand() : BaseCommand<JsonWebToken>
    {
        public string? RefreshToken { get; set; }
    }
}
