using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class AccountTransactionMapping : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMapping()
        {
            ToTable("Transaction", "cash");
            HasKey(v => v.Id);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasRequired(v => v.DebitAccount).WithMany(v => v.DebitAccountTransactions).HasForeignKey(v => v.DebitAccountId);
            HasRequired(v => v.CreditAccount).WithMany(v => v.CreditAccountTransactions).HasForeignKey(v => v.CreditAccountId);
        }
    }
}