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
    public class ChartRepository : IChartRepository
    {
        private readonly DataContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ChartRepository(DataContext context, IAccountRepository accountRepository, IMapper mapper)
        {
            _context = context;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public Result<Chart> ById(Guid id)
        {
            return Result.SuccessIfNotNull(_context.Charts.Find(id));
        }

        public IQueryable<Chart> All()
        {
            return _context.Charts.AsQueryable();
        }

        public Result<Chart> Add(CreateChartRequest request, Guid principal)
        {
            var chart =_mapper.Map<Chart>(request);
            chart.Id = Guid.NewGuid();
            chart.CreatedOn = DateTime.UtcNow;
            chart.CreatedBy = principal;
            _context.Charts.Add(chart);
            return Result.Success(chart);
        }

        public Result<Unit> Remove(Guid id)
        {
            return ById(id).Success(v =>
            {
                var accounts = _accountRepository.All(v.Id);

                if (accounts.Any()) return Result.Fail<Unit>();

                _context.Charts.Remove(v);
                return Result.Success();
            });
        }
        
        public Result<Chart> UpdateInfo(Guid id, UpdateChartInfoRequest request, Guid principal)
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