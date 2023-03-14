using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.BusinessLogic.Interfaces
{
    public interface ILectureService
    {
        void AddLecture(string lectureName, string stringDepartamentId);
        bool CheckIdIsExist(string lecID);
        List<Lecture> GetAllLecture();
        Lecture GetLectureById(int lecId);
        void DeleteLectureById(string id);
        List<int> CheckLectureIDListIsExist(List<int> inputList);
        void UpdateDepartamentById(int lecId, Lecture lectures, string newlistDepId);
        int GetLastIdOfLec();

    }
}
