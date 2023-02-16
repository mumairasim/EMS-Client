using System.Linq;
using Autofac;
using Autofac.Integration.WebApi;
using SMS.DATA.Implementation;
using SMS.DATA.Infrastructure;
using System.Reflection;
using System.Web.Http;
using AutoMapper;
using SMS.MAP;

namespace SMS.API.Registrar
{
    public class DependencyRegistrar
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<StoredProcCaller>().As<IStoredProcCaller>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load("SMS.SERVICES"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var autoMapperProfile = new MapperConfigurationInternal();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(autoMapperProfile);
            }));

            //register your mapper
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().SingleInstance();
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }

    }

}