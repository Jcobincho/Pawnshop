using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Responses
{
    
    public sealed class CreateUserResponse
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
