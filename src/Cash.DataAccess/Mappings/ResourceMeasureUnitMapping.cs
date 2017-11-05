using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class ResourceMeasureUnitMapping : EntityTypeConfiguration<ResourceMeasureUnit>
    {
        public ResourceMeasureUnitMapping()
        {
            ToTable(nameof(ResourceMeasureUnit));
            HasKey(v => v.Id);
        }
    }
}