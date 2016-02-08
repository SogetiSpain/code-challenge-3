using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System.Collections.Generic;
using Biblioteca;
using Moq;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class BibliotecaTests
    {
        List<Book> bookCatalog;
        List<User> userList;

        [TestInitialize]
        public void Initialize()
        {
            this.bookCatalog = new List<Book>();
            this.bookCatalog.Add(new Book()
            {
                ID = "HEA1",
                Name = "Head First Design Patterns",
                Pages = 250
            });
            this.bookCatalog.Add(new Book()
            {
                ID = "REF1",
                Name = "Refactoring to Patterns",
                Pages = 175
            });
            this.bookCatalog.Add(new Book()
            {
                ID = "LEA1",
                Name = "Leading High-Performant Teams",
                Pages = 105
            });
            this.bookCatalog.Add(new Book()
            {
                ID = "HEL1",
                Name = "Hello World",
                Pages = 6
            });
            this.userList = new List<User>();
            this.userList.Add(new User()
            {
                Username = "edin"
            });
        }


        [TestMethod]
        public void RegisterBook_ShouldInvokeRegisterOnStoreWhenEmpty()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.AddItem(It.IsAny<Book>())).Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Register(new Book() { ID = "ABC", Name = "Test Book" });

            store.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterBook_ShouldNotRegisterAlreadyExistingBook()
        {
            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetItem(It.Is<string>(i=>i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Register(new Book() { ID = "HEA1", Name = "Head First Design Patterns" });          
        }


        [TestMethod]
        public void RegisterBook_ShouldInvokeRegisterOnStoreWhenNonEmpty()
        {
            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetItem(It.Is<string>(i => i == "ABC"))).Returns( (LibraryItem) null);                
            store.Setup(x => x.AddItem(It.IsAny<Book>())).Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Register(new Book() { ID = "ABC", Name = "Test Book" });

            store.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Lend_ShouldThrowExceptionWhenBookIsNotInCatalog()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "abcd"))).Returns((LibraryItem)null);      

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("abcd", "edin", DateTime.Now);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Lend_ShouldThrowExceptionWhenUserIsNotValid()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());
            store.Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(true);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("HEA1", "inexistent user", DateTime.Now);
        }

        [TestMethod]        
        public void Lend_ShouldProceedWithValidUserAndItem()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                 .Returns(this.bookCatalog
                     .Where(b => b.ID == "HEA1")
                     .FirstOrDefault());
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(this.userList.Where(u => u.Username == "edin").First());
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(true);
            store
                .Setup(x => x.AddLending(It.IsAny<LibraryItem>(), It.IsAny<User>(), It.IsAny<DateTime>()))
                .Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("HEA1", "edin", DateTime.Now);

            store.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Lend_ShouldFailWithValidUserAndNonAvailableItem()
        {
            
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                 .Returns(this.bookCatalog
                     .Where(b => b.ID == "HEA1")
                     .FirstOrDefault());
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(this.userList.Where(u => u.Username == "edin").First());
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(false);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("HEA1", "edin", DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Lend_ShouldThrowExceptionWhenUserTriesToLendTooManyItems()
        {
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.AddLending(new LendingDetails(bookCatalog[0], user, DateTime.Now));
            user.AddLending(new LendingDetails(bookCatalog[1], user, DateTime.Now));
            user.AddLending(new LendingDetails(bookCatalog[2], user, DateTime.Now));

            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.IsAny<string>()))
                 .Returns((string id) => this.bookCatalog
                     .Where(b => b.ID == id)
                     .SingleOrDefault());
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(true);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("HEL1", "edin", DateTime.Now);
        }

        [TestMethod]
        public void Lend_ShouldProceedWhenUserTriesToLendThirdBook()
        {
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.AddLending(new LendingDetails(bookCatalog[0], user, DateTime.Now));
            user.AddLending(new LendingDetails(bookCatalog[1], user, DateTime.Now));

            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.IsAny<string>()))
                 .Returns((string id) => this.bookCatalog
                     .Where(b => b.ID == id)
                     .SingleOrDefault());
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(true);
            store
                .Setup(x => x.AddLending(It.IsAny<LibraryItem>(), It.IsAny<User>(), It.IsAny<DateTime>()))
                .Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Lend("HEL1", "edin", DateTime.Now);

            store.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Return_ShouldThrowExceptionWhenBookIsNotInCatalog()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "abcd"))).Returns((LibraryItem)null);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Return("abcd", "edin", DateTime.Now);   
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Return_ShouldThrowExceptionWhenUserIsNotValid()
        {
            var store = new Mock<IBibliotecaStore>();
            store.Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());
            store.Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(false);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Return("HEA1", "inexistent user", DateTime.Now);   
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Return_ShouldThrowExceptionWhenUserHasNoValidLending()
        {
            var user = this.userList.Where(u => u.Username == "edin").First();

            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(false);
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Return("HEA1", "edin", DateTime.Now);   
        }
                
        [TestMethod]
        public void Return_ShouldProceedWhenUserIsInTheReturnPeriod()
        {
            var returnDate = DateTime.Now.AddDays(15);
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.AddLending(new LendingDetails(bookCatalog[0], user, DateTime.Now));

            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(false);
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.RemoveLending(It.IsAny<LibraryItem>(), It.IsAny<User>(), It.IsAny<DateTime>()))
                .Verifiable();    

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Return("HEA1", "edin", returnDate);

            store.Verify();
        }

        [TestMethod]
        public void Return_ShouldBlockUserWhenExceedingTheReturnPeriod()
        {
            var returnDate = DateTime.Now.AddDays(40);
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.AddLending(new LendingDetails(bookCatalog[0], user, DateTime.Now));

            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetItem(It.Is<string>(i => i == "HEA1")))
                .Returns(this.bookCatalog
                    .Where(b => b.ID == "HEA1")
                    .FirstOrDefault());
            store
                .Setup(x => x.IsAvailableForLending(It.IsAny<LibraryItem>(), It.IsAny<DateTime>())).Returns(false);
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.RemoveLending(It.IsAny<LibraryItem>(), It.IsAny<User>(), It.IsAny<DateTime>()))
                .Verifiable();
            store
                .Setup(x => x.BlockUser(It.IsAny<User>()))
                .Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            var userStatus = biblioteca.Return("HEA1", "edin", returnDate);

            store.Verify();
            Assert.AreEqual(UserStatus.Blocked, userStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void Unblock_FailsWhenUserIsNotBlocked()
        {
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.Status = UserStatus.Normal;

            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.UnblockUser(It.IsAny <User>()))
                .Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Unblock("edin");           
        }


        [TestMethod]
        public void Unblock_ProceedsWhenUserIsBlocked()
        {
            var user = this.userList.Where(u => u.Username == "edin").First();
            user.Status = UserStatus.Blocked;

            var store = new Mock<IBibliotecaStore>();
            store
                .Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            store
                .Setup(x => x.UnblockUser(It.IsAny<User>()))
                .Verifiable();

            var biblioteca = new BibliotecaService(store.Object);
            biblioteca.Unblock("edin");

            store.Verify();
        }
    }
}
