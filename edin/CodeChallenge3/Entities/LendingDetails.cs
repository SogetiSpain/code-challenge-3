using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LendingDetails
    {
        public LibraryItem Item { get; private set; }
        public User User { get; private set; }
        public DateTime LendingDate { get; private set; }

        public LendingDetails(LibraryItem item, User user, DateTime lendingDate)
        {
            this.User = user;
            this.Item = item;
            this.LendingDate = lendingDate;
        }
    }
}
