using System;
using System.Web.Mvc;

namespace Cash.Web.Areas.Cash.Controllers
{
    public class ChartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}