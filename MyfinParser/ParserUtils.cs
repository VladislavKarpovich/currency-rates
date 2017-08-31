using System;

namespace MyfinParser
{
    internal static class ParserUtils
    {
        public static DateTime? ParseDateTime(string time)
        {
            var numbers = time.Split(':');

            var date = DateTime.Today;
            if (int.TryParse(numbers[0], out int hours) && int.TryParse(numbers[0], out int min))
            {
                date = date.AddHours(hours);
                date = date.AddMinutes(min);
                return date;
            }
            else
            {
                return null;
            }
        }

        public static double ParseRate(string html)
        {
            var validatedStr = html
                .Replace("\"", "")
                .Replace('.', ',')
                .Trim();

            if (double.TryParse(validatedStr, out double res))
            {
                return res;
            }
            return 0;
        }
    }
}
