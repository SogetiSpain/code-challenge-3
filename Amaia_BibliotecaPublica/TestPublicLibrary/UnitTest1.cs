using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IDataServiceLayer.Models;

namespace TestPublicLibrary
{
    [TestClass]
    public class UnitTest1
    {
        List<Document> documents;
        List<User> users;

        [TestInitialize]
        public void Initialize()
        {
            this.documents = new List<Document>();
            this.documents.Add(new Document()
            {
                Title = "Book 1"
            });
            this.documents.Add(new Document()
            {
                Title = "Book 1"
            });

            this.users = new List<User>();
            this.users.Add(new User()
            {
                Username = "Amaia"
               
            });
            this.users.Add(new User()
            {
                Username = "Jokin"
            });
        }
    }
}
