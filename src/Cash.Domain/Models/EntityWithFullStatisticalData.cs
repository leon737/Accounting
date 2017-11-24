using System;

namespace Cash.Domain.Models
{
    public class EntityWithFullStatisticalData : EntityWithStatisticalData
    {
        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual User ModifiedByUser { get; set; }
    }
}