using System;
using System.Collections.Generic;

namespace Cash.Domain.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User ModifiedByUser { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}