using System;
using System.Data.Entity;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public Result<Account> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.Accounts.Find(id));
        }

        public IQueryable<Account> All()
        {
            return _context.Accounts.AsQueryable();
        }

        public Result<Account> Add(Account account)
        {
            account.CreatedOn = DateTime.Now;
            _context.Accounts.Add(account);
            return Result.Success(account);
        }

        public Result<Unit> Remove(Guid id)
        {
            return ById(id).Success(v =>
            {
                if (v.Transactions.Any()) return Result.Fail<Unit>();

                _context.Accounts.Remove(v);
                return Result.Success();
            });
        }

        public Result<Account> Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            _context.Entry(account).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(account).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(account).Property(x => x.AccountType).IsModified = false;
            _context.Entry(account).Property(x => x.Transactions).IsModified = false;
            _context.Entry(account).Property(x => x.Balance).IsModified = false;
            return Result.Success(account);
        }
    }
}