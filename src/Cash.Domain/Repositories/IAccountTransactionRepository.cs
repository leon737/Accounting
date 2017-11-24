using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface IAccountTransactionRepository
    {
        Result<AccountTransaction> ById(Guid id);

        IQueryable<AccountTransaction> All(Guid accountId);

        Result<AccountTransaction> Add(CreateAccountTransactionRequest request);

        Result<Unit> Remove(Guid id);
    }
}