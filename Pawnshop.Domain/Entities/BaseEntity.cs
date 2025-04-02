using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } 
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public DateTime EditedAt { get; set; }
        public Guid EditedBy { get; set; }

        public void SetBaseEntity(Guid userId)
        {
            if(this.CreatedBy == Guid.Empty)
            {
                CreatedBy = userId;
                EditedBy = userId;
                CreatedAt = DateTime.Now;
                EditedAt = DateTime.Now;
            }
            else
            {
                EditedBy = userId;
                EditedAt = DateTime.Now;
            }
        }
    }
}
