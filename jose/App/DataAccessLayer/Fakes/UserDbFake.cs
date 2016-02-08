namespace Library.App.DataAccessLayer.Fake
{
    using Library.App.DataAccessLayer.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDbFake
    {
        private List<LibraryAppUserEntity> _userTable = new List<LibraryAppUserEntity> { 
                    new LibraryAppUserEntity () { Username = "jgonzalez" }, 
                    new LibraryAppUserEntity () { Username = "ekapic" }, 
                    new LibraryAppUserEntity () { Username = "rgrassi" }, 
                    new LibraryAppUserEntity () { Username = "mmontserrat" }, 
                    new LibraryAppUserEntity () { Username = "abaigorri" }, 
                    new LibraryAppUserEntity () { Username = "emoret" },
                    new LibraryAppUserEntity () { Username = "mgarriz" }, 
                    new LibraryAppUserEntity () { Username = "mbenaiges" } };

        public bool CreateUser(string usernameNewUser) 
        { 
            LibraryAppUserEntity newUser = new LibraryAppUserEntity ();
            bool find = false;
            newUser.Username = usernameNewUser;

            foreach (LibraryAppUserEntity libUser in _userTable)
            {
                if (libUser.Username == usernameNewUser)
                {
                    return false;
                }
            }

            if (find)
            {
                return false;
            }

            _userTable.Add(newUser);
            return true;        
            
            
        }

        public LibraryAppUserEntity GetUser(string usernameInput) 
        {
            LibraryAppUserEntity user = new LibraryAppUserEntity();

            foreach (LibraryAppUserEntity libUser in _userTable)
            {
                if (libUser.Username == usernameInput)
                {
                    user = libUser;
                    return user;
                }
            }

            return null;
        }

    }
}
