using System;
using System.Collections.Generic;
using System.Linq;

namespace Cash.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public decimal Workload { get; set; }

        public DateTime? StartDate { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual Task Parent { get; set; }

        public int? ParentId { get; set; }

        public int Importance { get; set; }

        public virtual ICollection<TaskResource> Resources { get; set; }

        public virtual ICollection<Task> Children { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User ModifiedByUser { get; set; }

        public bool Active { get; set; }

        public bool WorkloadAutoCalc { get; set; }

        public decimal GetCalculatedWorkload()
        {
            var sum = 0.0m;
            if (WorkloadAutoCalc && Children != null && Children.Any())
            {
                sum += Children.Where(x => x.Active).Sum(child => child.GetCalculatedWorkload());
            }
            return sum + (WorkloadAutoCalc ? 0.0m : Workload);
        }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public int TaskTypeId { get; set; }

        public virtual TaskType TaskType { get; set; }

        public int TaskStatusId { get; set; }

        public virtual TaskStatus TaskStatus { get; set; }
    }
}