using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Loan
    {
        public string Book { get; set; }
        public string User{ get; set; }
        public DateTime DateLoan { get; set; }
        public DateTime DateReturn { get; set; }

        public Loan(string book, string user, DateTime dateLoan)
        {
            this.Book = book;
            this.User = user;
            this.DateLoan = dateLoan;
            this.DateReturn = dateLoan.AddDays(30);
        }

        public Loan(string book, string user, DateTime dateLoan, DateTime dateReturn)
        {
            this.Book = book;
            this.User = user;
            this.DateLoan = dateLoan;
            this.DateReturn = dateReturn;
        }
    }
}
