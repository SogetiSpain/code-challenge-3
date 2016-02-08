using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public bool IsTaken { get; set; }

        public Book(string title, string code)
        {
            this.Title = title;
            this.Code = code;
            this.IsTaken = false;
        }

        public Book(string title, string code, bool taken)
        {
            this.Title = title;
            this.Code = code;
            this.IsTaken = taken;
        }
    }
}
