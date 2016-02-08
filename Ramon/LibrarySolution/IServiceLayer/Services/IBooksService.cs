using IServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceLayer.Services
{
    public interface IBooksService
    {
        Task<LibraryBooksOperationResult> ExecuteBookOperation(InfoRequest infoReq);
        
    }
}
