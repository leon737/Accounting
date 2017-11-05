using System.Web.Http;
using System.Web.Mvc;

namespace Cash.Web.Areas.Cash
{
    public class CashAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cash";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cash_default",
                "Cash/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.Routes.MapHttpRoute(
               name: "CashDefaultApi",
               routeTemplate: "Cash/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}