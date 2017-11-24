using Autofac;
using AutoMapper;
using Cash.DataAccess.Configuration;
using Cash.Domain.Configuration;

namespace Cash.Web.Configuration
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterModule<DataAccessModule>();

            builder.RegisterModule<DomainModule>();

            // automapper
            builder.RegisterType<WebApiAutomapperProfile>().As<Profile>();

            builder.Register(c =>
            {
                var profiles = c.Resolve<Profile[]>();

                return new MapperConfiguration(cfg =>
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });
            }).AsSelf();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}