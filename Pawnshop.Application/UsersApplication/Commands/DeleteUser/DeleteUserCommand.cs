using Pawnshop.Application.Common.Base;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.DeleteUser
{
    public sealed class DeleteUserCommand : BaseCommand<DeleteUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
