using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IServiceLayer.Models.LibraryValidator;

namespace IServiceLayer.Models
{
    public class LibraryBooksOperationResult
    {
        public ValidationMessages Message { get; set; }
        public bool Success { get; set; }
    }
}
