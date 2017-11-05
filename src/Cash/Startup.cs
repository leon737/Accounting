using System.Web.Http;
using Cash.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Cash.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            WebApiIocConfig.Configure(config);
            
            app.UseWebApi(config);
        }
    }
}
