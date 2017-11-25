using System;
using System.Linq;
using Cash.Domain.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISession _session;

        public AccountService(IAccountRepository accountRepository, ISession session)
        {
            _accountRepository = accountRepository;
            _session = session;
        }

        public IQueryable<Account> All(Guid chartId)
        {
            return _accountRepository.All(chartId);
        }

        public IResult UpdateAccount(Guid accountId, UpdateAccountInfoRequest request, Guid principal)
        {
            return _accountRepository.UpdateInfo(accountId, request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public IResult UpdateAccount(Guid accountId, UpdateAccountRequest request, Guid principal)
        {
            var account = _accountRepository.ById(accountId);
            if (account.Success(v => (!v.DebitAccountTransactions.Any() && !v.CreditAccountTransactions.Any() && v.Balance == 0m).ToResult()).IsFailed)
                return UpdateAccount(accountId, (UpdateAccountInfoRequest) request, principal);

            return _accountRepository.UpdateInfo(accountId, request, principal).Success(() =>
            {
                _session.SaveChanges();
                return Result.Success();
            });
        }

        public Result<Account> ById(Guid accountId)
        {
            return _accountRepository.ById(accountId);
        }

        //public IResult CreateChart(CreateChartRequest request, Guid principal)
        //{
        //    return _chartRepository.Add(request, principal).Success(() =>
        //    {
        //        _session.SaveChanges();
        //        return Result.Success();
        //    });
        //}

        //public IResult DeleteChart(Guid chartId)
        //{
        //    return _chartRepository.Remove(chartId).Success(() =>
        //    {
        //        _session.SaveChanges();
        //        return Result.Success();
        //    });
        //}
    }
}