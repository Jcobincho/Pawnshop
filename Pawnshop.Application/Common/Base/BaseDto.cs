namespace Pawnshop.Application.Common.Base
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime EditedAt { get; set; }
        public Guid EditedBy { get; set; }
    }
}
