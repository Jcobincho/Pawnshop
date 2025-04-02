using MediatR;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Responses;

namespace Pawnshop.Application.EmployeesApplication.Commands.AddEmployee
{
    public sealed class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>
    {
        private readonly IEmployeesCommandService _employeesCommandService;

        public AddEmployeeHandler(IEmployeesCommandService employeesCommandService)
        {
            _employeesCommandService = employeesCommandService;
        }

        public async Task<AddEmployeeResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = await _employeesCommandService.AddEmployeeAsync(request, cancellationToken);

            return new AddEmployeeResponse() 
            { 
                EmpoyeeId = employeeId 
            };
        }
    }
}
