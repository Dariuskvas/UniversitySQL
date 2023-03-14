using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.BusinessLogic.Interfaces
{
    public interface IDepartamentService
    {
        void AddDepartament(string depName);
        List<Departament> GetAllDepartaments();
        bool CheckIdIsExist(string depId);
        Departament GetDepartamentById(string depId);
        void DeleteDepartamentById(string depId);
        void UpdateDepartamentById(string id, Departament departament, string stringLectureID);
        List<int> CheckDepartamentIdListIsExist(List<int> inputList);
        bool CheckDbIsEmpty();
        int GetLastIdOfDep();
    }
}
