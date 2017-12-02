using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Cash.Web.Extensions
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString RequireJs(this HtmlHelper helper, string module)
        {
            return new MvcHtmlString($"<script>require([\"Modules/{module}\", \"domReady!\"], function(module) {{module.run();}});</script>");
        }

        public static MvcHtmlString WriteParentId<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> prop)
        {
            var operand = prop.Body as MemberExpression;
            var member = operand?.Member as PropertyInfo;

            var model = helper.ViewData.Model;
            var value = member?.GetValue(model) ?? "";

            return new MvcHtmlString($"<input type=\"hidden\" id=\"parentid\" value=\"{value}\" />");
        }
    }
}