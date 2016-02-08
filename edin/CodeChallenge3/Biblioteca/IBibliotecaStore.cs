using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public interface IBibliotecaStore
    {
        LibraryItem GetItem(string itemId);

        User GetUser(string userId);
        void BlockUser(User user);
        void UnblockUser(User user);


        void AddItem(LibraryItem item);

        void AddLending(LibraryItem item, User user, DateTime lendingDate);

        bool IsAvailableForLending(LibraryItem item, DateTime lendingDate);

        void RemoveLending(LibraryItem item, User user, DateTime returnDate);
       
    }
}
