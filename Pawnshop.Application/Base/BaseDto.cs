namespace Pawnshop.Application.Base
{
    public class BaseDto
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime EditedAt { get; set; }
        public Guid EditedBy { get; set; }
    }
}
