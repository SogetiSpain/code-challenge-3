namespace App.IoC.Resolver
{
    using Castle.Windsor;
    using IServiceLayer.Interfaces;

    public static class WindsorResolver
    {
        public static void ResolveDependencies(IWindsorContainer container, out IAccountService accountService, out IDocumentService bookService, out IUserService userService, out ILoanService loanService)
        {
            // Resolving
            accountService = container.Resolve<IAccountService>();
            bookService = container.Resolve<IDocumentService>();
            userService = container.Resolve<IUserService>();
            loanService = container.Resolve<ILoanService>();
        }
    }
}
