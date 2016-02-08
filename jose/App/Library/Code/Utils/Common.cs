
namespace Library.App.Program.Code.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Common
    {
        public static bool ValidateDate(string Date, out DateTime date)
        {
            date = DateTime.MinValue;
            try
            {
                date = DateTime.ParseExact(Date, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
                if (date < DateTime.Today) 
                {
                    throw new Exception();
                }
                    
            }                
            catch
            {
                return false;
            }
            return true;
        }

    }
}
