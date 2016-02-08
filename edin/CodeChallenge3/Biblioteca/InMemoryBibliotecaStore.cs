using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class InMemoryBibliotecaStore: IBibliotecaStore
    {
        private List<LibraryItem> catalog;
        private List<User> userList;
        private List<LendingDetails> lendings;

        protected object padLock = new object();

        public InMemoryBibliotecaStore(IEnumerable<User> users)
        {
            lock (this.padLock)
            {
                this.catalog = new List<LibraryItem>();
                this.userList = users.ToList();
                this.lendings = new List<LendingDetails>();
            }
        }

        public LibraryItem GetItem(string itemId)
        {
            var item = this.catalog.Where(x => x.ID.ToLowerInvariant() == itemId.ToLowerInvariant()).FirstOrDefault();
            return item;
        }

        public User GetUser(string userId)
        {
            var user = this.userList.Where(x => x.Username.ToLowerInvariant() == userId.ToLowerInvariant()).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El usuario {0} no está dado de alta en el sistema.", userId)
                    , "userId");
            }
            return user;
        }

        public void AddItem(Entities.LibraryItem item)
        {
            this.catalog.Add(item);
        }

        public void AddLending(LibraryItem item, User user, DateTime lendingDate)
        {
            var lendingDetails = new LendingDetails(item, user, lendingDate);
            this.lendings.Add(lendingDetails);

            user.AddLending(lendingDetails);
        }

        public bool IsAvailableForLending(LibraryItem item, DateTime lendingDate)
        {
            var alreadyExistingLending = this.lendings.Where(x => x.Item.ID == item.ID).FirstOrDefault();
            return alreadyExistingLending == null;
        }


        public void RemoveLending(LibraryItem item, User user, DateTime returnDate)
        {
            var lending = lendings.Where(i => i.Item.ID == item.ID).Single();
            this.lendings.Remove(lending);
            user.RemoveLending(lending);
        }


        public void BlockUser(User user)
        {
            user.Status = UserStatus.Blocked;
        }

        public void UnblockUser(User user)
        {
            user.Status = UserStatus.Normal;
        }
    }
}
