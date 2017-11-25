using System;
using System.Linq;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Functional.Fluent;
using Functional.Fluent.MonadicTypes;

namespace Cash.Domain.Services
{
    public interface IAccountService
    {
        IQueryable<Account> All(Guid chartId);

        IResult UpdateAccount(Guid accountId, UpdateAccountInfoRequest request, Guid principal);

        IResult UpdateAccount(Guid accountId, UpdateAccountRequest request, Guid principal);

        Result<Account> ById(Guid accountId);

        //IResult CreateChart(CreateChartRequest request, Guid principal);

        //IResult DeleteChart(Guid chartId);
    }
}