using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceLayer.Models
{
    public class InfoRequest
    {
        public string Username { get; set; }

        public string BookCode { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public OperationType OperationType { get; set; }
    }

    public enum OperationType
    {
        Register,
        Loan,
        Return
    }
}
