using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace Cash.Web
{
    public class MvcIoCConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}