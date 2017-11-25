using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Cash.Web.Binders;
using Cash.Web.ModelBinders;
using Cash.Web.Models;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Services;
using Cash.Web.ApiLibrary;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    [Route("api/chart")]
    public class ChartApiController : GridActionsApiControllerBase<Chart, ChartViewModel, UpdateChartInfoRequest, CreateChartRequest>
    {
        private readonly IChartService _chartService;

        public ChartApiController(IMapper mapper, IChartService chartService) : base(mapper)
        {
            _chartService = chartService;
        }
        
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            return Get(_chartService.All, loadOptions);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            return Put(_chartService.ById, _chartService.UpdateChart, form, principal);
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            return Post(_chartService.CreateChart, form, principal);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(FormDataCollection form)
        {
            return Delete(_chartService.DeleteChart, form);
        }
    }
}
