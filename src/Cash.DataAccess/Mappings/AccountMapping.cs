using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class AccountMapping : EntityTypeConfiguration<Account>
    {
        public AccountMapping()
        {
            ToTable(nameof(Account), "cash");
            HasKey(v => v.Id);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);
            HasOptional(v => v.LastUpdateByUser).WithMany().HasForeignKey(v => v.LastUpdatedBy);
            HasRequired(v => v.Currency).WithMany().HasForeignKey(v => v.CurrencyId);
            HasOptional(v => v.ParentAccount).WithMany(v => v.ChildAccounts).HasForeignKey(v => v.ParentAccountId);
        }
    }
}