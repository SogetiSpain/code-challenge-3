using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public User()
        {
            this.lendings = new List<LendingDetails>();
        }
        public string Username { get; set; }
        
        private List<LendingDetails> lendings;
        public IEnumerable<LendingDetails> Lendings
        {
            get
            {
                return lendings;
            }
        }

        public UserStatus Status { get; set; }

        public void AddLending(LendingDetails lending) {
            this.lendings.Add(lending);
        }

        public void RemoveLending(LendingDetails lending)
        {
            this.lendings.Remove(lending);
        }
    }
}
