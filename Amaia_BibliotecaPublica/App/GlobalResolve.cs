namespace App
{
    using Castle.Windsor;
    using IoC.Installer;
    using IoC.Resolver;
    using IServiceLayer.Interfaces;

    public class Global
    {
        private WindsorContainer container;

        public void Application_Start(out IAccountService accountService, out IDocumentService bookService, out IUserService userService, out ILoanService loanService)
        {
            container = new WindsorContainer();
            WindsorInstaller.Install(container);
            WindsorResolver.ResolveDependencies(container, out accountService, out bookService, out userService, out loanService);
        }
    }
}
