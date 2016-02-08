namespace Library.App.ServiceLayer.Implementation
{
    using Library.App.DataAccessLayer.Fake;
    using Library.App.DataAccessLayer.Entities;
    using Library.App.ServiceLayer.DTO;
    using Library.App.ServiceLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private static UserDbFake _dbUser = new UserDbFake();

        public UserService() 
        {
        }

        public LibraryAppUserDto GetUser(string username) 
        {
            LibraryAppUserEntity libUserEntity = _dbUser.GetUser(username);

            if (libUserEntity != null)
            {
                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.WriteLine("UserService - Usuario encontrado \n");
                //Console.ForegroundColor = ConsoleColor.White;

                LibraryAppUserDto libUserDto = new LibraryAppUserDto();
                libUserDto.Username = libUserEntity.Username;
                return (libUserDto);
            }
            else 
            {
                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.WriteLine("UserService - Usuario no encontrado \n");
                //Console.ForegroundColor = ConsoleColor.White;

                return null;
            }
        }
    }
}
