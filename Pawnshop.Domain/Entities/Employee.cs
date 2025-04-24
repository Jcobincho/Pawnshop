using Pawnshop.Domain.Entities;

namespace Pawnshop.Domain.Entitie
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
