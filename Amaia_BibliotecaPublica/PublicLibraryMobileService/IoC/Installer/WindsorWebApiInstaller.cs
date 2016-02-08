namespace PublicLibraryMobileService.IoC.Installer
{
    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using DataServiceLayer.Services;
    using IDataServiceLayer.Interfaces;
    using IServiceLayer.Interfaces;
    using ServiceLayer.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    public class WindsorWebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>().LifestylePerWebRequest());
            container.Register(Component.For<IAccountDataService>().ImplementedBy<AccountDataService>().LifestylePerWebRequest());

            container.Register(Component.For<IBookService>().ImplementedBy<BookService>().LifestylePerWebRequest());
            container.Register(Component.For<IBookDataService>().ImplementedBy<BookDataService>().LifestylePerWebRequest());
        }
    }
}
