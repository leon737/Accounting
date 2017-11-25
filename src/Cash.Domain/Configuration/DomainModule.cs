using Autofac;
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
        }
    }
}