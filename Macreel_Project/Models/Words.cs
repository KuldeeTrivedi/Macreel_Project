using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Macreel_Project.Models
{
    public static class Words
    {
        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) { return "zero"; }
            if (number < 0) { return "minus " + ConvertNumbertoWords(Math.Abs(number)); }
            string words = "";
            if ((number / 10000000) > 0) { words += ConvertNumbertoWords(number / 10000000) + " Crore "; number %= 10000000; }
            if ((number / 100000) > 0) { words += ConvertNumbertoWords(number / 100000) + " Lakh "; number %= 100000; }
            if ((number / 1000) > 0) { words += ConvertNumbertoWords(number / 1000) + " Thousand "; number %= 1000; }
            if ((number / 100) > 0) { words += ConvertNumbertoWords(number / 100) + " Hundred "; number %= 100; }
            if (number > 0)
            {
                if (words != "") { words += "and "; }
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "seventy", "Eighty", "Ninety" };
                if (number < 20) { words += unitsMap[number]; }
                else { words += tensMap[number / 10]; if ((number % 10) > 0) { words += " " + unitsMap[number % 10]; } }
            }
            return words;
        }
    }
}




