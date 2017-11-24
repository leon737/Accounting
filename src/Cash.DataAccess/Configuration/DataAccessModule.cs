using Autofac;
using AutoMapper;
using Cash.DataAccess.Contexts;
using Cash.DataAccess.Repositories;
using Cash.Domain.Contexts;
using Cash.Domain.Repositories;

namespace Cash.DataAccess.Configuration
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // data contexts
            builder.RegisterType<DataContext>().AsSelf().InstancePerRequest();

            // data session
            builder.RegisterType<Session>().As<ISession>().InstancePerRequest();

            // repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ChartRepository>().As<IChartRepository>();
            builder.RegisterType<CurrencyRepository>().As<ICurrencyRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountTransactionRepository>().As<IAccountTransactionRepository>();

            // mapper profiles
            builder.RegisterType<DataAccessMapperProfile>().As<Profile>();
        }
    }
}