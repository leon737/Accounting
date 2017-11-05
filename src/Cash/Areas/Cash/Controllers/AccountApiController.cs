using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cash.Domain.Services;

namespace Cash.Web.Areas.Cash.Controllers
{
    public class AccountApiController : ApiController
    {

        private readonly ITransliterationService _transliterationService;

        public AccountApiController(ITransliterationService transliterationService)
        {
            _transliterationService = transliterationService;
        }

        public string Get()
        {
            return "Hello, world!";
        }
    }
}
