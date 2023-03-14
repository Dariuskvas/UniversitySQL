using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.Respository.Interfaces
{
    public interface IDepartamentRespository
    {
        void AddDepartament(Departament departament);
        List<Departament> GetAllDepartametby();
        void Save();
        bool CheckIdIsExist(int depId);
        Departament GetDepartamentsById(int depId);
        void DeleteDepartamentById(int id);
        void UpdateDepartamentById(int id, Departament departament);
        void AddDepartamentLecturesById(int id, Lecture lecture);
        void DeleteDepartamentLecturesById(int id, Lecture lecture);
        int CountDep();
        int GetLastIdOfDep();
    }
}
