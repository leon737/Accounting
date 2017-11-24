using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class CurrencyMapping : EntityTypeConfiguration<Currency>
    {
        public CurrencyMapping()
        {
            ToTable(nameof(Currency), "cash");
            HasKey(v => v.Id);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);
        }
    }
}