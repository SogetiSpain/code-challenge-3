namespace Library.App.DataAccessLayer.Code
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Utils
    {
        public static DateTime CreateDate(string dateString)
        {
            DateTime date = DateTime.MinValue;
            date = DateTime.ParseExact(dateString, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
            if (date < DateTime.Today)
            {
                throw new Exception();
            }
            return date;
        }
    }
}
