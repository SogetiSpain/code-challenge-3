using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataServiceLayer.EF;
using DataServiceLayer.Repositories;
using IDataServiceLayer.Repositories;
using System.Data.Entity;
using ServiceLayer.Models.LibraryOperations;
using IServiceLayer.Models;
using IServiceLayer.Services;
using ServiceLayer.Services;

namespace ServiceLayer.IoC.Installer
{
    public class WindsorWebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<ILibraryOperations>()
                             .ImplementedBy<LoanBook>()
                             .Named(OperationType.Loan.ToString()).LifestylePerThread());
            container.Register(Component.For<ILibraryOperations>()
                             .ImplementedBy<ReturnBook>()
                             .Named(OperationType.Return.ToString()).LifestylePerThread());
            container.Register(Component.For<ILibraryOperations>()
                             .ImplementedBy<RegisterBook>()
                             .Named(OperationType.Register.ToString()).LifestylePerThread());
            container.Register(Component.For<IBooksService>()
                                .ImplementedBy<BooksService>().LifestylePerThread());
            container.Register(Component.For<IBooksRepository>()
                                .ImplementedBy<BooksRepository>().LifestylePerThread());
            container.Register(Component.For<IUsersRepository>()
                                .ImplementedBy<UsersRepository>().LifestylePerThread());
            container.Register(Component.For<DbContext>()
                                .ImplementedBy<azureRTCEntities>().LifestyleSingleton());


        }
    }
}
