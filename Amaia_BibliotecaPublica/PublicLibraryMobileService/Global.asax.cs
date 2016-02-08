using Castle.Windsor;
using PublicLibraryMobileService.IoC.Configure;
using System.Web.Http;
using System.Web.Routing;

namespace PublicLibraryMobileService
{
    public class WebApiApplication : System.Web.HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer container;

        public IWindsorContainer Container
        {
            get { return container; }
        }

        protected void Application_Start()
        {
            container = new WindsorContainer();
            WebApiConfig.Register();
            ConfigureIoC.Configure(container);
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}