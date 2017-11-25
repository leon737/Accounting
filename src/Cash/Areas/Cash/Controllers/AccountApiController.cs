using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Cash.Domain.Models;
using Cash.Domain.Requests;
using Cash.Domain.Services;
using Cash.Web.ApiLibrary;
using Cash.Web.Areas.Cash.Models;
using Cash.Web.Binders;
using Cash.Web.ModelBinders;

namespace Cash.Web.Areas.Cash.Controllers
{
    [Authorize]
    [Route("api/account/{id}")]
    public class AccountApiController : GridActionsApiControllerBase<Account, AccountViewModel, UpdateAccountRequest, CreateAccountRequest>
    {
        private readonly IAccountService _accountService;

        public AccountApiController(IMapper mapper, IAccountService accountService) : base(mapper)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, Guid id)
        {
            return Get(() => _accountService.All(id), loadOptions);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form, [ModelBinder(typeof(PrincipalModelBinder))] ClaimsPrincipal principal)
        {
            return Put(_accountService.ById, _accountService.UpdateAccount, form, principal);
        }
    }
}
