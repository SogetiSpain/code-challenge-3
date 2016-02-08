using IServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.LibraryOperations
{
    public interface ILibraryOperations
    {
        Task<LibraryBooksOperationResult> ExecuteOperation(InfoRequest infoRequest);
    }
}
