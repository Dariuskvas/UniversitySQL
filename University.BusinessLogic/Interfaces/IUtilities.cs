using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BusinessLogic.Interfaces
{
    public interface IUtilities
    {
        bool CheckValue(string MenuChoise);
        bool CheckIsInt(string insert);
        List<int> ConverteStringToIntList(string input);
    }
}
