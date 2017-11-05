using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class TaskTypeMapping : EntityTypeConfiguration<TaskType>
    {
        public TaskTypeMapping()
        {
            ToTable(nameof(TaskType));
            HasKey(v => v.Id);
        }
    }
}