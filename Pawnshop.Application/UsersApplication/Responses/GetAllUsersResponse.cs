using Pawnshop.Application.UsersApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Responses
{
    public sealed class GetAllUsersResponse
    {
        public List<GetAllUsersDto> Users { get; set; }
    }
}
