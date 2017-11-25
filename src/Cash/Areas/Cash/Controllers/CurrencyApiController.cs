using System.Web.Http;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Services;
using Cash.Web.ApiLibrary;
using Cash.Web.Areas.Cash.Models;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    [Route("api/currency")]
    public class CurrencyApiController : GridApiControllerBase<Currency, CurrencyViewModel, UpdateCurrencyInfoRequest, CreateCurrencyRequest>
    {
        public CurrencyApiController(IMapper mapper, ICurrencyService currencyService) 
            : base(mapper, currencyService.All, currencyService.ById, currencyService.UpdateCurrency, currencyService.CreateCurrency, currencyService.DeleteCurrency)
        {
        }
    }
}
