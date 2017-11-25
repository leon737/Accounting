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
            CreateMap<Chart, ChartViewModel>();

            CreateMap<ChartViewModel, UpdateChartInfoRequest>();

            CreateMap<ChartViewModel, CreateChartRequest>();
        }
    }
}