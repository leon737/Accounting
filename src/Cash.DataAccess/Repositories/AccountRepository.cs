using System;
using System.Linq;
using AutoMapper;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;
using Cash.DataAccess.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Requests;

namespace Cash.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IMapper _mapper;

        public AccountRepository(DataContext context, IAccountTransactionRepository accountTransactionRepository, IMapper mapper)
        {
            _context = context;
            _accountTransactionRepository = accountTransactionRepository;
            _mapper = mapper;
        }

        public Result<Account> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.Accounts.Find(id));
        }

        public IQueryable<Account> All(Guid chartId)
        {
            return _context.Accounts.Where(x => x.ChartId == chartId);
        }

        public Result<Account> Add(CreateAccountRequest request, Guid principal)
        {
            var account = _mapper.Map<Account>(request);
            account.Id = Guid.NewGuid();
            account.CreatedOn = DateTime.UtcNow;
            account.CreatedBy = principal;
            _context.Accounts.Add(account);
            return Result.Success(account);
        }

        public Result<Unit> Remove(Guid id)
        {
            return ById(id).Success(v =>
            {
                var transactions = _accountTransactionRepository.All(v.Id);

                if (transactions.Any()) return Result.Fail<Unit>();

                _context.Accounts.Remove(v);
                return Result.Success();
            });
        }
        
        public Result<Account> UpdateInfo(Guid id, UpdateAccountInfoRequest request, Guid principal)
        {
            return ById(id).Success(v =>
            {
                _mapper.Map(request, v);
                v.ModifiedOn = DateTime.UtcNow;
                v.ModifiedBy = principal;
                return Result.Success(v);
            });
        }

        public Result<Account> Lock(Guid id, bool locked, Guid principal)
        {
            return ById(id).Success(v =>
            {
                v.Locked = locked;
                v.ModifiedOn = DateTime.UtcNow;
                v.ModifiedBy = principal;
                return Result.Success(v);
            });
        }

        public Result<Account> UpdateBalance(Guid id, decimal amount, Guid principal)
        {
            return ById(id).Success(v =>
            {
                v.Balance += amount;
                v.LastUpdatedOn = DateTime.UtcNow;
                v.LastUpdatedBy = principal;
                return Result.Success(v);
            });
        }
    }
}