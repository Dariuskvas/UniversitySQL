using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.Respository.Interfaces
{
    public interface IStudentRespository
    {
        void AddStudent(Student student);
        void Save();
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        bool CheckStudentIdIsExist(int id);
        void DeleteStudentById(int id);
        void UpdateStudentById(int id, Student student);
    }
}
