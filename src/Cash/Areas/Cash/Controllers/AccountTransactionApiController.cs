using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Services;
using Cash.Web.ApiLibrary;
using Cash.Web.Areas.Cash.Models;
using Cash.Web.Binders;
using Cash.Web.ModelBinders;
using DevExtreme.AspNet.Data;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    [Route("api/transaction/{id}")]
    public class AccountTransactionApiController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IAccountTransactionService _service;

        public AccountTransactionApiController(IMapper mapper, IAccountTransactionService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, Guid id)
        {
            //return Get(() => _service.All(id), loadOptions);
            return Request.CreateResponse(DataSourceLoader.Load(_service.All(id)
                .Select(v => new AccountTransactionRelatedToAccountViewModel
                {
                    Id = v.Id,
                    Date = v.Date,
                    Amount = v.CreditAccountId == id ? v.CreditAmount : v.DebitAmount,
                    PreBalance = v.CreditAccountId == id ? v.PreCreditAccountBalance : v.PreDebitAccountBalance,
                    PostBalance = v.CreditAccountId == id ? v.PostCreditAccountBalance : v.PostDebitAccountBalance,
                    CorrespondingAccountId = v.CreditAccountId == id ? v.DebitAccountId : v.CreditAccountId,
                    RelationFrom = v.CreditAccountId == id ? BaseAccountRelationFrom.Credit : BaseAccountRelationFrom.Debit,
                    CreatedBy = v.CreatedByUser.UserName,
                    CreatedOn = v.CreatedOn
                })
                , loadOptions));
        }

        [Route("api/transaction/create")]
        [HttpPost]
        public HttpResponseMessage Create(CreateTransactionRequest request, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            var serviceRequest = _mapper.Map<CreateAccountTransactionRequest>(request);

            var claim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No valid claims found");

            var result = _service.CreateAccountTransaction(serviceRequest, Guid.Parse(claim.Value));

            return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<CreateTransactionResponse>(result));
        }

    }
}
