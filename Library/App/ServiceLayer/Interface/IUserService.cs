namespace Library.App.ServiceLayer.Interface
{
    using Library.App.ServiceLayer.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserService
    {
        LibraryAppUserDto GetUser(string username);

    }
}
