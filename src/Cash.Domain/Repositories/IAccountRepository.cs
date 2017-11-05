using System;
using System.Linq;
using Cash.Domain.Models;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface IAccountRepository
    {
        Result<Account> ById(Guid id);

        IQueryable<Account> All();

        Result<Account> Add(Account account);

        Result<Unit> Remove(Guid id);

        Result<Account> Update(Account account);
    }
}