using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        void AddStudent(Student student);
        void UpdateStudent(string id, Student student);
        List<Student> GetAllStudents();
        Student GetStudentById(int id);

        bool CheckIdIsExist(string id);

        void DeleteStudentById(string id);
    }
}
