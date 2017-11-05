namespace Cash.Domain.Models
{
    public class TaskStatusTransition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SourceStatusId { get; set; }

        public int TargetStatusId { get; set; }

        public virtual TaskStatus SourceStatus { get; set; }

        public virtual TaskStatus TargetStatus { get; set; }
    }
}