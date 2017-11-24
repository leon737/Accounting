using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface IChartRepository
    {
        Result<Chart> ById(Guid id);

        IQueryable<Chart> All();

        Result<Chart> Add(CreateChartRequest request, Guid principal);

        Result<Unit> Remove(Guid id);

        Result<Chart> UpdateInfo(Guid id, UpdateChartInfoRequest request, Guid principal);

    }
}