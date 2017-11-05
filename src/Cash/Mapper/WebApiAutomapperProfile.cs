using System.Linq;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Repositories;
using Cash.Domain.Services;
using Cash.Web.Models;
using Newtonsoft.Json;

namespace Cash.Web.Mapper
{
    public class WebApiAutomapperProfile : Profile
    {
        public WebApiAutomapperProfile()
        {
            
        }
    }
}