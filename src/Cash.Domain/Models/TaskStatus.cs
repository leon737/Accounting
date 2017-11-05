using System.Collections.Generic;

namespace Cash.Domain.Models
{
    public class TaskStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TaskStatusTransition> Transitions { get; set; }
    }
}