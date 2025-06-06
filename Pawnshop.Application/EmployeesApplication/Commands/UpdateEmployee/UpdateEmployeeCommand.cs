﻿using Pawnshop.Application.Common.Base;
using Pawnshop.Application.EmployeesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.EmployeesApplication.Commands.EditEmployee
{
    public sealed class UpdateEmployeeCommand : BaseCommand<UpdateEmployeeResponse>
    {
        [Required(ErrorMessage = "User identifier is required.")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Birthday date is required.")]
        public DateTime BirthDate { get; set; }
    }
}
