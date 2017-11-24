using System;

namespace Cash.Domain.Models
{
    public class EntityWithStatisticalData
    {
        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}