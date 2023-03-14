using University.BusinessLogic.Interfaces;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.BusinessLogic.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRespository _studentRespository;

        public StudentService(IStudentRespository studentRespository)
        {
            _studentRespository = studentRespository;
        }

        public void AddStudent(Student student)                                         //Sukurti nauja sudenta
        {
            _studentRespository.AddStudent(student);
            _studentRespository.Save();
            PrintSucsessMesage($"Studentas: << {student.fName} {student.lName} >> sukurtas sekmingai!");
        }

        public void UpdateStudent(string id, Student student)                          //Atnaujinti studenta
        {
            _studentRespository.UpdateStudentById(Convert.ToInt32(id), student);
            _studentRespository.Save();
            PrintSucsessMesage($"Studentas ID: << {id} >> atnaujintas sekmingai!");
        }

        public List<Student> GetAllStudents()                                           //Grazinti visus studentus
        {
            return _studentRespository.GetAllStudents();
        }

        public Student GetStudentById(int id)                                          //Grazinti konkretu studenta
        {
            return _studentRespository.GetStudentById(id);
        }

        public bool CheckIdIsExist(string id)                                           //Patikrinti ar pateiktas ID egzistuoja
        {
            return _studentRespository.CheckStudentIdIsExist(Convert.ToInt32(id));
        }

        public void DeleteStudentById(string id)                                        //Trinti studenta
        {
            _studentRespository.DeleteStudentById(Convert.ToInt16(id));
            _studentRespository.Save();
            PrintSucsessMesage($"Studentas ID: << {id} >> istrintas sekmingai!");
        }

        private void PrintSucsessMesage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(2000);
        }

    }

}
