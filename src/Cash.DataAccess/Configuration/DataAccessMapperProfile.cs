using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;

namespace Cash.DataAccess.Configuration
{
    public class DataAccessMapperProfile : Profile
    {
        public DataAccessMapperProfile()
        {
            CreateMap<UpdateChartInfoRequest, Chart>();

            CreateMap<CreateChartRequest, Chart>();

            CreateMap<UpdateCurrencyInfoRequest, Currency>();

            CreateMap<CreateCurrencyRequest, Currency>();

            CreateMap<UpdateAccountInfoRequest, Account>();

            CreateMap<CreateAccountRequest, Account>();

            CreateMap<CreateAccountTransactionRequest, AccountTransaction>();
        }

    }
}
