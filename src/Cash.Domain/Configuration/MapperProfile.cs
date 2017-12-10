using AutoMapper;
using Cash.Domain.Requests;

namespace Cash.Domain.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateAccountTransactionRequest, CreateAccountTransactionBalanceRequest>();
        }
    }
}