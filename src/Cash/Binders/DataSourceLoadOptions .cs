using System.Web.Http.ModelBinding;
using DevExtreme.AspNet.Data;

namespace Cash.Web.Binders
{
    [ModelBinder(typeof(DataSourceLoadOptionsHttpBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase { }
}