using System;
using System.Linq;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Repositories
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly DataContext _context;

        public AccountTransactionRepository(DataContext context)
        {
            _context = context;
        }

        public Result<AccountTransaction> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.AccountTransactions.Find(id));
        }

        public IQueryable<AccountTransaction> All(Guid accountId)
        {
            return _context.AccountTransactions.Where(x => x.Debit == accountId || x.Credit == accountId).AsQueryable();
        }

        public Result<AccountTransaction> Add(AccountTransaction accountTransaction)
        {
            accountTransaction.CreatedOn = DateTime.Now;
            _context.AccountTransactions.Add(accountTransaction);
            return Result.Success(accountTransaction);
        }

        public Result<Unit> Remove(Guid id)
        {
            return ById(id).Success(v =>
            {
                _context.AccountTransactions.Remove(v);
                return Result.Success();
            });
        }
    }
}