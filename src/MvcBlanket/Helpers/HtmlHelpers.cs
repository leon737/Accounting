using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcBlanket.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString SortLink(this HtmlHelper helper, string title, string action, string column, SortDirection direction)
        {
            var sortOptions = (GridSortOptions) helper.ViewBag.GridSortOptions;

            return helper.ActionLink(title, action, new
            {
                column,
                direction = column == sortOptions.Column && direction == sortOptions.Direction ? InvertDirection(direction) : direction
            });
        }

        private static SortDirection InvertDirection(SortDirection direction)
        {
            return direction == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
        }
    }
}