using Pawnshop.Application.EmployeesApplication.Dto.DtoExtension;
using Pawnshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Dto.DtoExtension
{
    public static class UsersDtoExtension
    {
        public static GetAllUsersDto UserPraseToDto(this Users users)
        {

            return new GetAllUsersDto
            {
                UserId = users.Id,
                UserName = users.UserName,
                Email = users.Email,
                Employee = users.Employee?.EmployeePraseToDto()
            };
        }
    }
}
