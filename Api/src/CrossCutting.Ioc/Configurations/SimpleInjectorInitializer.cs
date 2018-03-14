using System.Web.Http;
using CrossCutting.Ioc.Containers;
using SharedKernel.DomainEvents;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace CrossCutting.Ioc.Configurations
{
    public class SimpleInjectorInitializer
    {
        private static Container _container;

        public static void Initialize(HttpConfiguration config)
        {
            _container = new Container();

            _container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(_container);

            _container.RegisterWebApiControllers(config);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
            DomainEvent.Container = new IoCContainer(config.DependencyResolver);

            _container.Verify();
        }

        public static object GetInstance<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        private static void InitializeContainer(Container container)
        {
            Bootstraper.RegisterServices(container);
        }
    }
}
