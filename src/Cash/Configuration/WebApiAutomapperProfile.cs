using System;
using System.Linq;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Results;
using Cash.Web.Areas.Cash.Models;
using Cash.Web.Extensions;

namespace Cash.Web.Configuration
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile()
        {
            CreateMap<Chart, ChartViewModel>()
                .ForMember(m => m.CreatedBy, c => c.MapFrom(v => v.CreatedByUser.UserName))
                .ForMember(m => m.ModifiedBy, c => c.MapFrom(v => v.ModifiedByUser.UserName));

            CreateMap<ChartViewModel, UpdateChartInfoRequest>();

            CreateMap<ChartViewModel, CreateChartRequest>();

            CreateMap<Account, AccountViewModel>()
                .ForMember(m => m.HasTransactions, c=> c.MapFrom(v => v.CreditAccountTransactions.Any() || v.DebitAccountTransactions.Any()))
                .ForMember(m => m.CreatedBy, c => c.MapFrom(v => v.CreatedByUser.UserName))
                .ForMember(m => m.ModifiedBy, c => c.MapFrom(v => v.ModifiedByUser.UserName))
                .ForMember(m => m.LastUpdatedBy, c => c.MapFrom(v => v.LastUpdateByUser.UserName));

            CreateMap<AccountViewModel, UpdateAccountRequest>();

            CreateMap<AccountViewModel, CreateAccountRequest>();

            CreateMap<Currency, CurrencyViewModel>()
                .ForMember(m => m.CreatedBy, c => c.MapFrom(v => v.CreatedByUser.UserName))
                .ForMember(m => m.ModifiedBy, c => c.MapFrom(v => v.ModifiedByUser.UserName));

            CreateMap<CurrencyViewModel, UpdateCurrencyInfoRequest>();

            CreateMap<CurrencyViewModel, CreateCurrencyRequest>();

            CreateMap<CreateTransactionRequest, CreateAccountTransactionRequest>()
                .ForMember(m => m.CreditAccountId, c => c.MapFrom(v => v.CreditAccount))
                .ForMember(m => m.DebitAccountId, c => c.MapFrom(v => v.DebitAccount))
                .ForMember(m => m.CreditAmount, c => c.MapFrom(v => v.Amount))
                .ForMember(m => m.DebitAmount, c => c.MapFrom(v => v.Amount))
                .ForMember(m => m.CurrencyRate, c => c.UseValue(1.0m))
                .ForMember(m => m.Date, c => c.MapFrom(v => v.Date != null ? new DateTime().FromUnixTime(v.Date.Value) : DateTime.UtcNow));

            CreateMap<CreateAccountTransactionResult, CreateTransactionResponse>();

            CreateMap<AccountTransaction, AccountTransactionViewModel>();
        }
    }
}