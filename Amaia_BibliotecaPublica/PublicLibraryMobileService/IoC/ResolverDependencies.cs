using Castle.MicroKernel.Registration;
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

namespace PublicLibraryMobileService.IoC
{
    public class ResolverDependencies
    {
        private WindsorContainer container;

        public ResolverDependencies()
        {
            this.container = new WindsorContainer();
        }

        public void RegisterDependencies()
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());

            // Registering
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>());
            container.Register(Component.For<IAccountDataService>().ImplementedBy<AccountDataService>());

            container.Register(Component.For<IBookService>().ImplementedBy<BookService>());
            container.Register(Component.For<IBookDataService>().ImplementedBy<BookDataService>());
        }

        public void ResolveDependencies(out IAccountService accountService, out IBookService bookService)
        {
            // Resolving
            accountService = container.Resolve<IAccountService>();
            bookService = container.Resolve<IBookService>();
        }
    }
}
