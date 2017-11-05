using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class TaskMapping : EntityTypeConfiguration<Task>
    {
        public TaskMapping()
        {
            ToTable(nameof(Task));
            HasKey(v => v.Id);
            Property(v => v.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(v => v.ParentId).HasColumnName("ParentId");
            HasOptional(v => v.Parent).WithMany(v => v.Children).HasForeignKey(v => v.ParentId);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);
            HasRequired(v => v.Project).WithMany(v => v.Tasks).HasForeignKey(v => v.ProjectId);
            HasRequired(v => v.TaskType).WithMany().HasForeignKey(v => v.TaskTypeId);
            HasRequired(v => v.TaskStatus).WithMany().HasForeignKey(v => v.TaskStatusId);
        }
    }
}