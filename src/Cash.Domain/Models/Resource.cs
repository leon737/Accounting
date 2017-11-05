using System;

namespace Cash.Domain.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MeasureUnitId { get; set; }

        public virtual ResourceMeasureUnit MeasureUnit { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string AdditionalInfo { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User ModifiedByUser { get; set; }

        public decimal? Price { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

    }
}