using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Cash.Web.ModelBinders
{
    public class PrincipalModelBinder : IModelBinder
    {
        /// <summary> Привязка пользователя из запроса </summary>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (!typeof(ClaimsPrincipal).IsAssignableFrom(bindingContext.ModelType))
            {
                return false;
            }

            bindingContext.Model = actionContext.RequestContext.Principal;

            return true;
        }
    }
}