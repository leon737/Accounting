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
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ISession _session;

        public CurrencyService(ICurrencyRepository currencyRepository, ISession session)
        {
            _currencyRepository = currencyRepository;
            _session = session;
        }

        public IQueryable<Currency> All()
        {
            return _currencyRepository.All();
        }

        //public IResult UpdateChart(Guid chartId, UpdateChartInfoRequest request, Guid principal)
        //{
        //    return _chartRepository.UpdateInfo(chartId, request, principal).Success(() =>
        //    {
        //        _session.SaveChanges();
        //        return Result.Success();
        //    });
        //}

        //public Result<Chart> ById(Guid chartId)
        //{
        //    return _chartRepository.ById(chartId);
        //}

        //public IResult CreateChart(CreateChartRequest request, Guid principal)
        //{
        //    return _chartRepository.Add(request, principal).Success(() =>
        //    {
        //        _session.SaveChanges();
        //        return Result.Success();
        //    });
        //}

        //public IResult DeleteChart(Guid chartId)
        //{
        //    return _chartRepository.Remove(chartId).Success(() =>
        //    {
        //        _session.SaveChanges();
        //        return Result.Success();
        //    });
        //}
    }
}