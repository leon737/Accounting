using System;
using System.Web.Mvc;
using Cash.Domain.Services;

namespace Cash.Web.Areas.Cash.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Transactions(Guid id)
        {
            ViewBag.Id = id;
            var account = _accountService.ById(id).Value;
            ViewBag.ChartId = account.ChartId;
            return View();
        }
    }
}