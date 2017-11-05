using System;

namespace Cash.Domain.Models
{
    public class TaskResource
    {
        public int Id { get; set; }

        public virtual Task Task { get; set; }

        public virtual Resource Resource { get; set; }

        public int TaskId { get; set; }

        public int ResourceId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User ModifiedByUser { get; set; }
    }
}