using Pawnshop.Application.Base;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.Logout
{
    public class LogoutCommand : BaseCommand<LogoutResponse>
    {
        public string RefreshToken { get; set; }
    }
}
