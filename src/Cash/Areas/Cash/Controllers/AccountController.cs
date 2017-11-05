using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cash.Web.Areas.Cash.Controllers
{
    public class AccountController : Controller
    {
        // GET: Cash/Account
        public ActionResult Index()
        {
            return Content("HELLO!");
        }
    }
}