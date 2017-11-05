using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class AccountTransactionMapping : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMapping()
        {
            ToTable(nameof(AccountTransaction), "Cash");
            HasKey(v => v.Id);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasRequired(v => v.DebitAccount).WithMany().HasForeignKey(v => v.Debit);
            HasRequired(v => v.CreditAccount).WithMany().HasForeignKey(v => v.Credit);
        }
    }
}