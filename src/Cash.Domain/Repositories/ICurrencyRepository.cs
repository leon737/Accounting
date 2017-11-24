using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Result<Currency> ById(Guid id);

        IQueryable<Currency> All();

        Result<Currency> Add(CreateCurrencyRequest request, Guid principal);

        Result<Unit> Remove(Guid id);

        Result<Currency> UpdateInfo(Guid id, UpdateCurrencyInfoRequest request, Guid principal);

    }
}