namespace CrossCutting.Utils
{
    using System;

    public class ConsoleDatas
    {
        public string GetData(string displayText, string exception)
        {
            Console.WriteLine(displayText);
            var value = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine(exception);
                return GetData(displayText, exception);
            }

            return value;
        }
    }
}
