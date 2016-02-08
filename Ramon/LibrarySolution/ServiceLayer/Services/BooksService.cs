using Castle.Windsor;
using IDataServiceLayer.Repositories;
using IServiceLayer.Models;
using IServiceLayer.Services;
using ServiceLayer.IoC;
using ServiceLayer.IoC.Configure;
using ServiceLayer.Models.LibraryOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BooksService : IBooksService
    {

        private static IWindsorContainer container;

        public BooksService()
        {
            container = WindsorCastleContainer.Instance;
        }

        public async Task<LibraryBooksOperationResult> ExecuteBookOperation(InfoRequest infoReq)
        {
            return await container.Resolve<ILibraryOperations>(infoReq.OperationType.ToString()).ExecuteOperation(infoReq);
        }
    }
}
