using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Domain.Entities
{
    public class Employees
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
