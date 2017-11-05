using Autofac;
using AutoMapper;
using Cash.DataAccess.Contexts;
using Cash.DataAccess.Repositories;
using Cash.Domain.Contexts;
using Cash.Domain.Repositories;
using Cash.Domain.Services;
using Cash.Domain.Services.Impl;
using Cash.Web.Mapper;

namespace Cash.Web.IoC
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // data contexts
            builder.RegisterType<DataContext>().AsSelf().InstancePerRequest();


            // data session
            builder.RegisterType<Session>().As<ISession>().InstancePerRequest();

            // repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountTransactionRepository>().As<IAccountTransactionRepository>();

            // services

            builder.RegisterType<TransliterationService>().As<ITransliterationService>();

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

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}