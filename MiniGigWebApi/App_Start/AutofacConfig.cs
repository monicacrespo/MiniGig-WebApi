namespace MiniGigWebApi
{
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using AutoMapper;
    using DisconnectedGenericRepository;
    using MiniGigWebApi.Data;
    using MiniGigWebApi.Services;
    using MiniGigWebApi.SharedKernel.Data;

    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GigMappingProfile());
            });

            bldr.RegisterInstance(config.CreateMapper())
              .As<IMapper>()
              .SingleInstance();

            bldr.RegisterType<MiniGigContext>()
                .As<System.Data.Entity.DbContext>()            
                .InstancePerRequest();

            // Repositories
            bldr.RegisterGeneric(typeof(GenericRepository<>))
               .As(typeof(IGenericRepository<>))           
               .InstancePerDependency();
            
            bldr.RegisterType<GigService>()
                .As<IGigService>()
                .InstancePerRequest();

            bldr.RegisterType<GigService>()
                .As<IGigService>()
                .InstancePerRequest();
        }
    }
}