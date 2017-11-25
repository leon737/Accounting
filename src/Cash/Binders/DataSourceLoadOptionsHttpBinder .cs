using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using DevExtreme.AspNet.Data.Helpers;

namespace Cash.Web.Binders
{
    class DataSourceLoadOptionsHttpBinder : IModelBinder
    {

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var loadOptions = new DataSourceLoadOptions();
            DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key)?.AttemptedValue);
            bindingContext.Model = loadOptions;
            return true;
        }

    }
}