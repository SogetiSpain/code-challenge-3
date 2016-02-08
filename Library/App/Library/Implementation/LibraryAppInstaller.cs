
namespace Library.App.Program.Implementation
{
    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.App.Program.Interface;

    public class LibraryAppInstaller : IWindsorInstaller
    {

        /// <summary>
        /// Installs the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILibraryApp>().ImplementedBy<LibraryApp>().Interceptors(InterceptorReference.ForKey("Trace")).Anywhere.LifestyleTransient());
        }

    }
}
