using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using University.BusinessLogic.Interfaces;


namespace University.BusinessLogic
{
    public class Utilities: IUtilities
    {
        private readonly string pattern = @"(using\s+System\.Reflection|Type\.GetType|Assembly\.Load)";
        
        public bool CheckValue(string input)
        {
            if (!Regex.IsMatch(input, pattern) && !string.IsNullOrWhiteSpace(input))
                return true;
            return false;
        }

        public bool CheckIsInt(string input)
        {
            if (int.TryParse(input, out int number))
                return true;
            return false;
        }

        public List<int> ConverteStringToIntList(string input)
        {
            List<int> intList = new List<int>();

            foreach (Match match in Regex.Matches(input, @"[0-9]+"))
            {
                int num;
                if (int.TryParse(match.Value, out num))
                {
                    intList.Add(num);
                }
            }
            return intList;
        }
    }
}
