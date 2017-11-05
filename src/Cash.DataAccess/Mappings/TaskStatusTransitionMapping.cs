using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class TaskStatusTransitionMapping : EntityTypeConfiguration<TaskStatusTransition>
    {
        public TaskStatusTransitionMapping()
        {
            ToTable(nameof(TaskStatusTransition));
            HasKey(v => v.Id);
            HasRequired(v => v.SourceStatus).WithMany(v => v.Transitions).HasForeignKey(v => v.SourceStatusId);
            HasRequired(v => v.TargetStatus).WithMany().HasForeignKey(v => v.TargetStatusId);
        }
    }
}