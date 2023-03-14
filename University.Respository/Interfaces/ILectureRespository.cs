using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.Respository.Interfaces
{
    public interface ILectureRespository
    {
        void AddLecture(Lecture lecture, List<Departament> depList);
        void Save();
        bool CheckIdIsExist(int lecID);
        List<Lecture> GetAllLecture();
        Lecture GetLectureById(int lecId);
        void DeleteLectureById(int id);
        void UpdateLectureById(int id, Lecture lecture);
        void AddLectureDepartamentsById(int id, Departament departament);
        void DeleteLectureDepartamentsById(int id, Departament departament);
        int GetLastIdOfLec();

    }
}
