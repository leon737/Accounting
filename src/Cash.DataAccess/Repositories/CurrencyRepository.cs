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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CurrencyRepository(DataContext context, IAccountRepository accountRepository, IMapper mapper)
        {
            _context = context;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public Result<Currency> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.Currencies.Find(id));
        }

        public IQueryable<Currency> All()
        {
            return _context.Currencies.AsQueryable();
        }

        public Result<Currency> Add(CreateCurrencyRequest request, Guid principal)
        {
            var currency = _mapper.Map<Currency>(request);
            currency.Id = Guid.NewGuid();
            currency.CreatedOn = DateTime.UtcNow;
            currency.CreatedBy = principal;
            _context.Currencies.Add(currency);
            return Result.Success(currency);
        }

        public Result<Unit> Remove(Guid id)
        {
            return ById(id).Success(v =>
            {
                var accounts = _accountRepository.All(v.Id);

                if (accounts.Any()) return Result.Fail<Unit>();

                _context.Currencies.Remove(v);
                return Result.Success();
            });
        }
        
        public Result<Currency> UpdateInfo(Guid id, UpdateCurrencyInfoRequest request, Guid principal)
        {
            return ById(id).Success(v =>
            {
                _mapper.Map(request, v);
                v.ModifiedOn = DateTime.UtcNow;
                v.ModifiedBy = principal;
                return Result.Success(v);
            });
        }
    }
}