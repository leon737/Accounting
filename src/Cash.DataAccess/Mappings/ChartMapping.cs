using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class ChartMapping : EntityTypeConfiguration<Chart>
    {
        public ChartMapping()
        {
            ToTable(nameof(Chart), "cash");
            HasKey(v => v.Id);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);
        }
    }
}