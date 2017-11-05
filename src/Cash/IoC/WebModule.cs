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
            builder.RegisterType<ResourceMeasureUnitRepository>().As<IResourceMeasureUnitRepository>();
            builder.RegisterType<ResourceRepository>().As<IResourceRepository>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterType<TaskResourceRepository>().As<ITaskResourceRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<TaskTypeRepository>().As<ITaskTypeRepository>();
            builder.RegisterType<TaskStatusRepository>().As<ITaskStatusRepository>();

            // services
            builder.RegisterType<TaskService>().As<ITaskService>();
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