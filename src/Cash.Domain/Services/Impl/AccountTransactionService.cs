using System;
using System.Linq;
using AutoMapper;
using Cash.Domain.Contexts;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Requests;
using Cash.Domain.Results;
using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services.Impl
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public AccountTransactionService(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository, ISession session, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _session = session;
            _mapper = mapper;
        }


        public CreateAccountTransactionResult CreateAccountTransaction(CreateAccountTransactionRequest request, Guid principal)
        {
            if (request.CreditAccountId == request.DebitAccountId)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.CreditAndDebitAccountsAreSame);

            if (request.CreditAmount <= 0.0m)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.AmountIsZero);

            if (request.DebitAmount <= 0.0m)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.AmountIsZero);

            var creditAccount = _accountRepository.ById(request.CreditAccountId);
            var debitAccount = _accountRepository.ById(request.DebitAccountId);

            if (creditAccount.IsFailed)
                throw new ArgumentException();

            if (debitAccount.IsFailed)
                throw new ArgumentException();

            if (creditAccount.Value.Locked)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.CreditAccountLocked);

            if (debitAccount.Value.Locked)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.DebitAccountLocked);

            var lastCreditAccountTransaction = _accountTransactionRepository.LastByDate(request.CreditAccountId);
            var lastDebitAccountTransaction = _accountTransactionRepository.LastByDate(request.DebitAccountId);

            if (lastCreditAccountTransaction.IsSucceed && lastCreditAccountTransaction.Value.Date >= request.Date)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.CreditAccountTimelineViolation);

            if (lastDebitAccountTransaction.IsSucceed && lastDebitAccountTransaction.Value.Date >= request.Date)
                return CreateAccountTransactionResult.Fail(CreateTransactionError.DebitAccountTimelineViolation);
            
            var newCreditAccountBalance = CalculateCreditAccountBalance(creditAccount, request.CreditAmount);
            var newDebitAccountBalance = CalculateDebitAccountBalance(debitAccount, request.DebitAmount);

            if (!ValidateAccount(creditAccount, newCreditAccountBalance))
                return CreateAccountTransactionResult.Fail(CreateTransactionError.CreditAccountConstraintViolation);

            if (!ValidateAccount(debitAccount, newDebitAccountBalance))
                return CreateAccountTransactionResult.Fail(CreateTransactionError.DebitAccountConstraintViolation);

            var balanceRequest = _mapper.Map<CreateAccountTransactionBalanceRequest>(request);
            balanceRequest.PreCreditAccountBalance = creditAccount.Value.Balance;
            balanceRequest.PreDebitAccountBalance = debitAccount.Value.Balance;
            balanceRequest.PostCreditAccountBalance = newCreditAccountBalance;
            balanceRequest.PostDebitAccountBalance = newDebitAccountBalance;

            var transactionId = AddAccountTransaction(balanceRequest, principal);

            UpdateAccount(creditAccount, newCreditAccountBalance, principal);
            UpdateAccount(debitAccount, newDebitAccountBalance, principal);

            _session.SaveChanges();
            return CreateAccountTransactionResult.Success(transactionId);
        }

        public IQueryable<AccountTransaction> All(Guid accountId)
        {
            return _accountTransactionRepository.All(accountId);
        }

        public Result<AccountTransaction> ById(Guid transactionId)
        {
            return _accountTransactionRepository.ById(transactionId);
        }

        private decimal CalculateCreditAccountBalance(Account creditAccount, decimal amount)
       {
           return creditAccount.Balance - amount;
       }

        private decimal CalculateDebitAccountBalance(Account debitAccount, decimal amount)
        {
            return debitAccount.Balance + amount;
        }

        private bool ValidateAccount(Account account, decimal balance)
        {
            return account.Type.Match()
                .With(AccountType.Active, balance >= 0)
                .With(AccountType.Passive, balance <= 0)
                .Else(true)
                .Do();
        }

        private void UpdateAccount(Account account, decimal newBalance, Guid principal)
        {
            _accountRepository.UpdateBalance(account.Id, newBalance, principal);
        }

        private Guid AddAccountTransaction(CreateAccountTransactionBalanceRequest request, Guid principal)
        {
            return _accountTransactionRepository.Add(request, principal).Value.Id;
        }

    }
}