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

        public IResult UpdateCurrency(Guid currencyId, UpdateCurrencyInfoRequest request, Guid principal)
        {
            return _currencyRepository.UpdateInfo(currencyId, request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public Result<Currency> ById(Guid currencyId)
        {
            return _currencyRepository.ById(currencyId);
        }

        public IResult CreateCurrency(CreateCurrencyRequest request, Guid principal)
        {
            return _currencyRepository.Add(request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public IResult DeleteCurrency(Guid currencyId)
        {
            return _currencyRepository.Remove(currencyId).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }
    }
}