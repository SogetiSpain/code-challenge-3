using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book: LibraryItem
    {
        public int Pages { get; set; }

        public override int MaximumNumberOfItemsInLending()
        {
            return 3;
        }
    }
}
