using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BusinessLogic.Interfaces
{
    public interface IDbService
    {
        void ImportDataFromFile(JToken json);
    }
}
