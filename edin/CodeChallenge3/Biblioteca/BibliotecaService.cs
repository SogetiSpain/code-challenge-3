using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class BibliotecaService
    {
        private IBibliotecaStore store;

        public BibliotecaService(IBibliotecaStore store)
        {
            this.store = store;
        }

        public void Register(LibraryItem item)
        {
            var checkitem = this.store.GetItem(item.ID);

            if (checkitem != null)
            {
                throw new ArgumentException(
                    String.Format("El elemento con el ID {0} ya existe en el catálogo.", item.ID)
                    , "item");
            }
            this.store.AddItem(item);
        }


        public void Lend(string itemId, string userId, DateTime date)
        {
            var item = this.store.GetItem(itemId);
            if (item == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El elemento con el ID {0} no existe en el catálogo.", itemId)
                    , "itemId");
            }
            
            if (!this.store.IsAvailableForLending(item, date))
            {
                throw new ArgumentException(
                   String.Format("El elemento con el ID {0} ya está en préstamo.", itemId)
                   , "itemId");
            }

            var user = this.store.GetUser(userId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El usuario {0} no está dado de alta en el sistema.", userId)
                    , "userId");
            }
            if (!UserCanCurrentlyLendItem(item, user))
            {
                throw new InvalidOperationException(
                    String.Format("El usuario {0} no puede coger prestado ahora el elemento {1}.", user.Username, item.ID)
                    );
            }


            this.store.AddLending(item, user, date);
        }




        public UserStatus Return(string itemId, string userId, DateTime date)
        {
            var item = this.store.GetItem(itemId);
            if (item == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El elemento con el ID {0} no existe en el catálogo.", itemId)
                    , "itemId");
            }

            if (this.store.IsAvailableForLending(item, date))
            {
                throw new ArgumentException(
                   String.Format("El elemento con el ID {0} no está prestado.", itemId)
                   , "itemId");
            }

            var user = this.store.GetUser(userId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El usuario {0} no está dado de alta en el sistema.", userId)
                    , "userId");
            }
            if (!UserHasOutstandingLending(item, user))
            {
                throw new InvalidOperationException(
                    String.Format("El usuario {0} no tiene prestado actualmente el elemento {1}.", user.Username, item.ID)
                    );
            }
            var result = UserStatus.Normal;
            var shouldBlock = UserHasExceededLendingLength(item, user, date);

            this.store.RemoveLending(item, user, date);
            if (shouldBlock)
            {
                this.store.BlockUser(user);
                result = UserStatus.Blocked;
            }
            return result;
        }




        public void Unblock(string userId)
        {
            var user = this.store.GetUser(userId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("El usuario {0} no está dado de alta en el sistema.", userId)
                    , "userId");
            }
            if (user.Status != UserStatus.Blocked)
            {
                throw new InvalidOperationException(String.Format("El usuario {0} no está bloqueado.", user.Username));
            }
            this.store.UnblockUser(user);
        }

        private bool UserCanCurrentlyLendItem(LibraryItem item, User user)
        {
            bool result = false;

            if (user.Status == UserStatus.Blocked)
            {
                return result;
            }

            if (user.Lendings.Count(l => l.Item.GetType() == item.GetType()) < item.MaximumNumberOfItemsInLending())
            {
                result = true;
            }

            return result;
        }

        private bool UserHasOutstandingLending(LibraryItem item, User user)
        {
            bool result = false;
            if (user.Lendings.Count() == 0)
            {
                return false;
            }
            if (user.Lendings.Any(x => x.Item.ID == item.ID))
            {
                result = true;
            }
            return result;
        }

        private bool UserHasExceededLendingLength(LibraryItem item, User user, DateTime date)
        {
            bool result = false;
            var lending = user.Lendings.Where(l => l.Item.ID == item.ID).Single();
            var elapsedDays = (date - lending.LendingDate).Days;
            if (elapsedDays > item.DaysToLend())
            {
                result = true;
            }
            return result;
        }



    }
}
