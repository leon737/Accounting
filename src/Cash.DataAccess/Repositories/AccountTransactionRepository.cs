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
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountTransactionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result<AccountTransaction> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.AccountTransactions.Find(id));
        }

        public IQueryable<AccountTransaction> All(Guid accountId)
        {
            return _context.AccountTransactions.Where(x => x.DebitAccountId == accountId || x.CreditAccountId == accountId).AsQueryable();
        }

        public Result<AccountTransaction> Add(CreateAccountTransactionRequest request, Guid principal)
        {
            var accountTransaction = _mapper.Map<AccountTransaction>(request);
            accountTransaction.Id = Guid.NewGuid();
            accountTransaction.CreatedOn = request.Date;
            accountTransaction.CreatedBy = principal;
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