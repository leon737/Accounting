using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Cash.Web
{
    public class WebApiIocConfig
    {
        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterWebApiFilterProvider(configuration);
            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}