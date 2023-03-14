using Newtonsoft.Json.Linq;
using University.BusinessLogic.Interfaces;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.BusinessLogic.Service
{
    public class DbService : IDbService
    {
        private readonly IDepartamentRespository _departamentRespository;
        private readonly ILectureRespository _lectureRespository;
        private readonly IStudentRespository _studentRespository;
        private readonly IUtilities _utilities;

        public DbService(IDepartamentRespository departamentRespository, ILectureRespository lectureRespository, IStudentRespository studentRespository, IUtilities utilities)
        {
            _departamentRespository = departamentRespository;
            _lectureRespository = lectureRespository;
            _studentRespository = studentRespository;
            _utilities = utilities;
        }

        //Pradiniu duomenu ikelimas is JSON

        public void ImportDataFromFile(JToken firstData)
        {
            ImportDepartaments(firstData);
            ImportLectures(firstData);
            ImportStudents(firstData);
            Console.WriteLine("Testi - ENTER");
            Console.ReadLine();
        }

        private void ImportDepartaments(JToken json)                         //sukelti fakultetus
        {
            foreach (JToken department in json["Departament"])
            {
                string departmentName = department["name"].ToString();
                Departament dep = new Departament();
                dep.name = departmentName;
                _departamentRespository.AddDepartament(dep);
            }
            _departamentRespository.Save();
            Console.WriteLine("Fakultetai sukurti sekmingai!");
        }

        private void ImportLectures(JToken json)                            //Sukelti paskaitas
        {
            foreach (JToken lecture in json["Lecture"])
            {
                string lectureName = lecture["name"].ToString();
                string stringOfNumber = lecture["departamentString"].ToString();
                string lecDepId = ChangeStringToLectureIdStrig(stringOfNumber);

                Lecture lec = new Lecture();
                lec.name = lectureName;

                List<Departament> dep = new List<Departament>();
                _utilities.ConverteStringToIntList(lecDepId).ForEach(id => dep.Add(_departamentRespository.GetDepartamentsById(id)));
                _lectureRespository.AddLecture(lec, dep);
            }
            _lectureRespository.Save();
            Console.WriteLine("Paskaitos sukurtos sekmingai!");
        }

        private void ImportStudents(JToken json)                            //Sukelti studentus
        {

            foreach (JToken student in json["Student"])
            {
                Student stud = new Student();

                stud.fName = student["fname"].ToString();
                stud.lName = student["lname"].ToString();
                int lastDepId = _departamentRespository.GetLastIdOfDep();                   //Pasiimu paskutini Departamento ID
                int constDepId = int.Parse(student["departament"].ToString());
                stud.departaments = _departamentRespository.GetDepartamentsById(lastDepId - constDepId);
                _studentRespository.AddStudent(stud);
            }
            _studentRespository.Save();
            Console.WriteLine("Studentai sukurti sekmingai!");
        }

        private string ChangeStringToLectureIdStrig(string input)                           //Susikuriu departanemtu ID stringa pagaluotas JASON vertes, kuriems priskiriu paskaita
        {
            int lastLecId = _departamentRespository.GetLastIdOfDep();
            return string.Join(" ", _utilities.ConverteStringToIntList(input).Select(n => lastLecId - n));
        }
    }
}
