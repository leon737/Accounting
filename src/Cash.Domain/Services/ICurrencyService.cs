using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services
{
    public interface ICurrencyService
    {
        IQueryable<Currency> All();

        IResult UpdateCurrency(Guid currencyId, UpdateCurrencyInfoRequest request, Guid principal);

        Result<Currency> ById(Guid currencyId);

        IResult CreateCurrency(CreateCurrencyRequest request, Guid principal);

        IResult DeleteCurrency(Guid currencyId);
    }
}