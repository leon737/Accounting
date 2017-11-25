using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cash.Web.Areas.Cash.Controllers
{
    public class ChartController : Controller
    {
        // GET: Cash/Chart
        public ActionResult Index()
        {
            return View();
        }
    }
}