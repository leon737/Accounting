using Autofac;
using AutoMapper;
using Cash.Domain.Services;
using Cash.Domain.Services.Impl;

namespace Cash.Domain.Configuration
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // services
            builder.RegisterType<TransliterationService>().As<ITransliterationService>();
            builder.RegisterType<ChartService>().As<IChartService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>();
            builder.RegisterType<AccountTransactionService>().As<IAccountTransactionService>();

            builder.RegisterType<MapperProfile>().As<Profile>();
        }
    }
}