using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Results;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services
{
    public interface IAccountTransactionService
    {
        CreateAccountTransactionResult CreateAccountTransaction(CreateAccountTransactionRequest request, Guid principal);

        IQueryable<AccountTransaction> All(Guid accountId);

        Result<AccountTransaction> ById(Guid transactionId);
    }
}