using Infrastructure.Log.Interface;
using Infrastructure.Log.Implementation;
namespace Infrastructure.Log.Installer
{
    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Install dependencies for production environment
    /// </summary>
    public class Log4NetInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILoggerFactory>().ImplementedBy<LogFactory>().LifeStyle.Singleton);
        }
    }
}
