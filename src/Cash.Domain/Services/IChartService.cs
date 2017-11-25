using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services
{
    public interface IChartService
    {
        IQueryable<Chart> All();

        IResult UpdateChart(Guid chartId, UpdateChartInfoRequest request, Guid principal);

        Result<Chart> ById(Guid chartId);

        IResult CreateChart(CreateChartRequest request, Guid principal);

        IResult DeleteChart(Guid chartId);
    }
}