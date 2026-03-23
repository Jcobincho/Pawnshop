using Pawnshop.Application.Common.Base;
using Pawnshop.Application.UsersApplication.Responses;

namespace Pawnshop.Application.UsersApplication.Commands.Logout
{
    public class LogoutCommand : BaseCommand<LogoutResponse>
    {
        public string? RefreshToken { get; set; }
    }
}
