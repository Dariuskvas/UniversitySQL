using University.BusinessLogic.Interfaces;
using University.Presentation.Interfaces;
using University.Respository.Models;

namespace University.Presentation
{
    public class InputForNewSubjects : IInputForNewSubjects
    {
        private readonly IUtilities _utilities;
        private readonly IDepartamentService _departamentService;
        private readonly ILectureService _lectureService;
        private readonly IStudentService _studentService;

        public InputForNewSubjects(IUtilities utilities, IDepartamentService departamentService, ILectureService lectureService, IStudentService studentService)
        {
            _utilities = utilities;
            _departamentService = departamentService;
            _lectureService = lectureService;
            _studentService = studentService;
        }

        public void AddDepartamet()                                                         //Fakulteto sukurimas
        {
            Console.Clear();
            Console.WriteLine("Naujo fakulteto sukurimas.");
            Console.WriteLine("Iveskite fakulteto pavadinima: ");
            var depName = Console.ReadLine();

            if (depName != string.Empty)                                                   //Tikrinu ar ivestas reiksme
            {
                if (_utilities.CheckValue(depName) == true)                                //Tikrinu ar ivestas reiksme nera kodas ar tarpas
                {
                    _departamentService.AddDepartament(depName);                           
                }
                else
                {
                    Console.Clear();
                    PrintErrorMessage(depName);
                }
            }
        }

        public void AddLecture()
        {
            Console.Clear();
            Console.WriteLine("Naujos paskaitos sukurimas.");
            Console.WriteLine("Iveskite paskaitos pavadinima: ");
            var lectureName = Console.ReadLine();
            PrintAllDep();
            Console.WriteLine("Iveskite fakulteto ID, kuriems turi buti priskirta paskaita. PVZ: 1, 2, 3, 4 ir t.t.");
            var stringDepartamentID = Console.ReadLine();

            if (lectureName != string.Empty || stringDepartamentID != string.Empty)                     //Tikrinu ar ivestas reiksme
            {
                if (_utilities.CheckValue(lectureName) == true)                                         //Tikrinu ar ivestas reiksme nera kodas ar tarpas
                {
                    _lectureService.AddLecture(lectureName, stringDepartamentID);
                }
                else
                {
                    Console.Clear();
                    PrintErrorMessage(lectureName);
                }
            }

        }

        public void AddStudent()                                                //Prideti studenta
        {
            Console.Clear();
            Console.WriteLine("Naujo studento sukurimas.");
            Console.WriteLine("Iveskite studento varda: ");
            var studentName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Iveskite studento pavarde: ");
            var studentLastName = Console.ReadLine();
            Console.WriteLine();
            PrintAllDep();
            Console.WriteLine("Kuriam departamentui priskirti studenta, pasirinkite is saraso pagal ID: ");
            var depId = Console.ReadLine();

            if (studentName != string.Empty || studentLastName != string.Empty || depId != string.Empty)       //jei beint viena reiksme tuscia, nutraukiamas kurimas
            {
                if (CheckInputValidity(studentName, studentLastName, depId) == true)                           //tikrinu ar ivestos reiksmes tinkamos
                {
                    Student student = new Student                                                              //priskiriu Studentui reiksmes
                    {
                        fName = studentName,
                        lName = studentLastName,
                        departaments = _departamentService.GetDepartamentById(depId)                           //priskiriu studenta departamentui
                    };
                    _studentService.AddStudent(student);                                        
                }
                else
                {
                    Console.Clear();
                    PrintErrorMessage($"{studentName} arba {studentLastName} arba {depId}");
                }
            }
        }

        private bool CheckInputValidity(string studentName, string studentLastName, string depId)
        {
            return _utilities.CheckValue(studentName) == true && _utilities.CheckValue(studentLastName) == true && _utilities.CheckValue(depId) == true && _utilities.CheckIsInt(depId) && true && _departamentService.CheckIdIsExist(depId) == true;
        }

        private void PrintErrorMessage(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{input} - Netinkamas pasirinkimas.");
            Thread.Sleep(2000);
            Console.ResetColor();
        }

        private void PrintAllDep()
        {
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("Fakultetu sarasas");
            _departamentService.GetAllDepartaments().ForEach(depart => Console.WriteLine($"ID: {depart.id}   Pavadinimas: {depart.name}"));
            Console.WriteLine("--------------------------------------------------------------------");
        }

    }
}

