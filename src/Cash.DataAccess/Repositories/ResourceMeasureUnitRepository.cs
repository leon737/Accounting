using System.Collections.Generic;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class ResourceMeasureUnitRepository : IResourceMeasureUnitRepository
    {

        private readonly DataContext _context;

        public ResourceMeasureUnitRepository(DataContext context)
        {
            _context = context;
        }

        public Result<ResourceMeasureUnit> ById(int id) => Result.SuccessIfNotNull(_context.ResourceMeasureUnits.Find(id));

        public IReadOnlyList<ResourceMeasureUnit> All() => _context.ResourceMeasureUnits.AsReadOnlyList();
    }
}