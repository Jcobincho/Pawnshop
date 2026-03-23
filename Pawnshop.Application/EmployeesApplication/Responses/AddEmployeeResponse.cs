namespace Pawnshop.Application.EmployeesApplication.Responses
{
    public sealed class AddEmployeeResponse
    {
        public Guid EmpoyeeId { get; set; }
        public string Message { get; set; } = "Success.";
    }
}
