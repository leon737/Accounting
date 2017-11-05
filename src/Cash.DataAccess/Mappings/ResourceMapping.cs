using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class ResourceMapping : EntityTypeConfiguration<Resource>
    {
        public ResourceMapping()
        {
            ToTable(nameof(Resource));
            HasKey(v => v.Id);
            Property(v => v.MeasureUnitId).HasColumnName("MeasureUnit");
            HasRequired(v => v.MeasureUnit).WithMany().HasForeignKey(v => v.MeasureUnitId);
            HasRequired(v => v.CreatedByUser).WithMany().HasForeignKey(v => v.CreatedBy);
            HasOptional(v => v.ModifiedByUser).WithMany().HasForeignKey(v => v.ModifiedBy);
            HasRequired(v => v.Project).WithMany(v => v.Resources).HasForeignKey(v => v.ProjectId);
        }
    }
}