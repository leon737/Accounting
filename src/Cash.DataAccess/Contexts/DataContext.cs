using System.Data.Entity;
using Cash.Domain.Models;

namespace Cash.DataAccess.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext() : base(nameof(DataContext))
        {
            Database.SetInitializer<DataContext>(null);
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<TaskResource> TaskResources { get; set; }

        public DbSet<ResourceMeasureUnit> ResourceMeasureUnits { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }

        public DbSet<TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}