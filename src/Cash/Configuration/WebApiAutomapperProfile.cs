using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Web.Models;

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
        }
    }
}