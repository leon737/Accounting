using System.Web.Mvc;

namespace Cash.Web.Extensions
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString RequireJs(this HtmlHelper helper, string module)
        {
            return new MvcHtmlString($"<script>require([\"Modules/{module}\", \"domReady!\"], function(module) {{module.run();}});</script>");
        }
    }
}