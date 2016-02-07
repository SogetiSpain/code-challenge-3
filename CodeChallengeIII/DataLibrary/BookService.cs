using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class BookService
    {

        public List<Book> GetAllBooks()
        {
            string line;
            List<Book> bookList = new List<Book>();
            StreamReader file = new StreamReader("C:\\Books.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] lines = line.Split('\t');
                Book aux = new Book(lines[1], lines[0], bool.Parse(lines[2]));
                bookList.Add(aux);
            }
            file.Close();
            return bookList;
        }

        public void SaveAllBooks(List<Book> booklist)
        {
            StreamWriter file = new StreamWriter("C:\\Books.txt", false);
            foreach (Book a in booklist)
            {
                file.WriteLine(a.Code + "\t" + a.Title + "\t" + a.IsTaken);
            }
            file.Close();
        }

    }
}
