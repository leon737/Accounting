using System;
using System.Linq;
using Cash.Domain.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services.Impl
{
    public class ChartService : IChartService
    {
        private readonly IChartRepository _chartRepository;
        private readonly ISession _session;

        public ChartService(IChartRepository chartRepository, ISession session)
        {
            _chartRepository = chartRepository;
            _session = session;
        }

        public IQueryable<Chart> All()
        {
            return _chartRepository.All();
        }

        public IResult UpdateChart(Guid chartId, UpdateChartInfoRequest request, Guid principal)
        {
            return _chartRepository.UpdateInfo(chartId, request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public Result<Chart> ById(Guid chartId)
        {
            return _chartRepository.ById(chartId);
        }

        public IResult CreateChart(CreateChartRequest request, Guid principal)
        {
            return _chartRepository.Add(request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public IResult DeleteChart(Guid chartId)
        {
            return _chartRepository.Remove(chartId).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }
    }
}