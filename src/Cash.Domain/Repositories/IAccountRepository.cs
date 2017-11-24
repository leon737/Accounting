﻿using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Repositories
{
    public interface IAccountRepository
    {
        Result<Account> ById(Guid id);

        IQueryable<Account> All(Guid chartId);

        Result<Account> Add(CreateAccountRequest request, Guid principal);

        Result<Unit> Remove(Guid id);

        Result<Account> UpdateInfo(Guid id, UpdateAccountInfoRequest request, Guid principal);

        Result<Account> Lock(Guid id, bool locked, Guid principal);

        Result<Account> UpdateBalance(Guid id, decimal amount, Guid principal);
    }
}