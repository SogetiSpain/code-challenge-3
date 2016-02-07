using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class LoanService
    {
        public List<Loan> GetAllLoans(){
            List<Loan> loanList = new List<Loan>();
            string line;
            
            StreamReader file = new StreamReader("C:\\Loans.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] lines = line.Split('\t');
                Loan aux = new Loan(lines[0], lines[1], DateTime.Parse(lines[2]), DateTime.Parse(lines[3]));
                loanList.Add(aux);
            }
            file.Close();
            return loanList;
        }

        public void SaveAllLoans(List<Loan> loanList)
        {
            StreamWriter file = new StreamWriter("C:\\Loans.txt", false);
            foreach (Loan a in loanList)
            {
                file.WriteLine(a.Book + "\t" + a.User + "\t" + a.DateLoan + "\t" + a.DateReturn);
            }
            file.Close();
        } 
    }
}
