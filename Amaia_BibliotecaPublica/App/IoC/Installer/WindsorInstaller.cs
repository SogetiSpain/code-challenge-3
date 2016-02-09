namespace App.IoC.Installer
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using DataServiceLayer.Services;
    using IDataServiceLayer.Interfaces;
    using IServiceLayer.Interfaces;
    using ServiceLayer.Services;

    public static class WindsorInstaller
    {
        public static void Install(IWindsorContainer container)
        {
            // Registering
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>());
            container.Register(Component.For<IAccountDataService>().ImplementedBy<AccountDataService>());

            container.Register(Component.For<IDocumentService>().ImplementedBy<DocumentService>());
            container.Register(Component.For<IDocumentDataService>().ImplementedBy<DocumentDataService>());

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            container.Register(Component.For<IUserDataService>().ImplementedBy<UserDataService>());

            container.Register(Component.For<ILoanService>().ImplementedBy<LoanService>());
            container.Register(Component.For<ILoanDataService>().ImplementedBy<LoanDataService>());
        }
    }
}