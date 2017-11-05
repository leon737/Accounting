using System;
using System.Linq;
using Cash.Domain.Models;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface IAccountTransactionRepository
    {
        Result<AccountTransaction> ById(Guid id);

        IQueryable<AccountTransaction> All(Guid accountId);

        Result<AccountTransaction> Add(AccountTransaction accountTransaction);

        Result<Unit> Remove(Guid id);
    }
}