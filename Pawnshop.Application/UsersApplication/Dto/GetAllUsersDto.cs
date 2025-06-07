using Pawnshop.Application.Common.Base;
using Pawnshop.Application.EmployeesApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Dto
{
    public sealed class GetAllUsersDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public EmployeeDto? Employee { get; set; }
    }
}
