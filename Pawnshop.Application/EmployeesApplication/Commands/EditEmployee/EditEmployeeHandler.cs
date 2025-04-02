using MediatR;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Commands.EditEmployee
{
    public sealed class EditEmployeeHandler : IRequestHandler<EditEmployeeCommand, EditEmployeeResponse>
    {
        private readonly IEmployeesCommandService _employeesCommandService;

        public EditEmployeeHandler(IEmployeesCommandService employeesCommandService)
        {
            _employeesCommandService = employeesCommandService;
        }

        public async Task<EditEmployeeResponse> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeesCommandService.EditEmployeeAsync(request, cancellationToken);

            return new EditEmployeeResponse();
        }
    }
}
