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

        public DbSet<Chart> Charts { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}