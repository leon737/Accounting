using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class TaskResourceMapping : EntityTypeConfiguration<TaskResource>
    {
        public TaskResourceMapping()
        {
            ToTable(nameof(TaskResource));
            HasKey(v => v.Id);
            HasRequired(v => v.Task).WithMany(v => v.Resources).HasForeignKey(v => v.TaskId);
            HasRequired(v => v.Resource).WithMany().HasForeignKey(v => v.ResourceId);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);

        }
    }
}