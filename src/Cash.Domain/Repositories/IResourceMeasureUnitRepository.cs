using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;
using Cash.Domain.Models;

namespace Cash.Domain.Repositories
{
    public interface IResourceMeasureUnitRepository
    {
        Result<ResourceMeasureUnit> ById(int id);

        IReadOnlyList<ResourceMeasureUnit> All();
    }
}