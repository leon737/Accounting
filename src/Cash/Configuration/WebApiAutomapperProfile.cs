using System.Linq;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Web.Areas.Cash.Models;

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

        }
    }
}