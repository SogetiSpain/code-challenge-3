using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class LibraryItem
    {
        public string Name { get; set; }
        public string ID { get; set; }

        public abstract int MaximumNumberOfItemsInLending();
        public virtual int DaysToLend()
        {
            return 30;
        }

    }
}
