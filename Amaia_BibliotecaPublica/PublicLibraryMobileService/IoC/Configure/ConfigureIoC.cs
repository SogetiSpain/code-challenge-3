namespace PublicLibraryMobileService.IoC.Configure
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using Resolver;
    using System.Web.Http;

    public static class ConfigureIoC
    {
        public static void Configure(IWindsorContainer container)
        {
            container.Install(FromAssembly.This());
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(container);
        }
    }
}
