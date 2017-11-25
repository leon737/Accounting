using System.Web.Http;
using AutoMapper;
using Cash.Web.Models;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Services;
using Cash.Web.ApiLibrary;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    [Route("api/chart")]
    public class ChartApiController : GridApiControllerBase<Chart, ChartViewModel, UpdateChartInfoRequest, CreateChartRequest>
    {
        public ChartApiController(IMapper mapper, IChartService chartService) 
            : base(mapper, chartService.All, chartService.ById, chartService.UpdateChart, chartService.CreateChart, chartService.DeleteChart)
        {
        }
    }
}
