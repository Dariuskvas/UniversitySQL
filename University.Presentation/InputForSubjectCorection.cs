using University.BusinessLogic.Interfaces;
using University.Presentation.Interfaces;
using University.Respository.Models;

namespace University.Presentation
{
    public class InputForSubjectCorection : IInputForSubjectCorection
    {
        private readonly IUtilities _utilities;
        private readonly IStudentService _studentService;
        private readonly IDepartamentService _departamentService;
        private readonly ILectureService _lectureService;

        public InputForSubjectCorection(IUtilities utilities, IStudentService studentService, IDepartamentService departamentService, ILectureService lectureService)
        {
            _utilities = utilities;
            _studentService = studentService;
            _departamentService = departamentService;
            _lectureService = lectureService;
        }

        // ---------------------------------  studentai --------------------------------------------------------------------------

        public void UpdateStudent()                                                                //Studento koregavimas
        {
            Console.Clear();
            PrintAllStudents();                                                                    //Atsispausdinu visus studentus
            Console.WriteLine("Pasirinkite studento ID kuri norite koreguoti:");
            var studId = Console.ReadLine();
            Console.Clear();
            if (studId != string.Empty)
            {
                if (CheckStudentID(studId) == true)                                                    //Patikrinu ar ivestas tinkamas studento ID
                {
                    var student = _studentService.GetStudentById(Convert.ToInt32(studId));             //pasiemu pasirinkto studento informacija
                    Console.WriteLine($"pasirinkto studento informacija");
                    Console.WriteLine($"ID: {student.id},  studentas: {student.fName} {student.lName}. Priklauso fakulteto ID: {student.departaments.id}, fakulteto pav.: {student.departaments.name}");
                    SelectActionWithStudentId(studId);                                                 //kokie veiksmai bus atliekami su pasirinktu studento ID
                }
                else
                {
                    PrintErrorMessageWrongInput();
                    UpdateStudent();
                }
            }
        }

        // veiksmai su pasirinktu studento ID
        private void SelectActionWithStudentId(string studId)
        {
            PrintOptionsMenu();
            var input = Console.ReadLine();
            Console.Clear();
            if (_utilities.CheckValue(input) == true && _utilities.CheckIsInt(input) == true)      //tikrinu ivesta pasirinkima
            {
                if (input == "1")
                {
                    _studentService.DeleteStudentById(studId);              //studento istrinimas pagal ID
                }
                else if (input == "2")
                {
                    ChangeStudentInformation(studId);                       //studento informacijos keitimas pagal pasirinkta ID
                }
            }
            else
            {
                PrintErrorMessageWrongInput();
                UpdateStudent();                                            //Grista i meniu vienu lygiu aukstin
            }
        }

        private bool CheckStudentID(string studId)                          //tikrinamas studento ID: ar ne kenkejiskas kodas, ar ivestas skaicius, ar egzistuoja
        {
            return _utilities.CheckValue(studId) && _utilities.CheckIsInt(studId) && _studentService.CheckIdIsExist(studId);
        }


        private void ChangeStudentInformation(string studId)                 //Studento informacijos surinkimas jam koreguoti
        {
            Console.WriteLine("Naujas studento vardas:");
            var newFirstName = Console.ReadLine();
            Console.WriteLine("Naujas studento pavarde:");
            var newLastName = Console.ReadLine();
            PrintAllDepartamentsToConsole();                                 //Atspausdinu visu fakultetu lista
            Console.WriteLine("Naujas studento fakulteto ID, pasirinkite iš sarašo: ");
            var newDepId = Console.ReadLine();

            if (newFirstName != string.Empty || newLastName != string.Empty || newDepId != string.Empty)
            {
                if (CheckNewStudentData(newFirstName, newLastName, newDepId) == true)
                {
                    Student student = new Student
                    {
                        fName = newFirstName,
                        lName = newLastName,
                        departaments = _departamentService.GetDepartamentById(newDepId)
                    };
                    _studentService.UpdateStudent(studId, student);
                }
                else
                {
                    PrintErrorMessageWrongInput();
                    UpdateStudent();

                }
            }

        }


        private bool CheckNewStudentData(string newFirstName, string newLastName, string newDepId)          // Patikrinu ar naujai suvesti Studento Duomenys yra tinkami
        {
            return _utilities.CheckValue(newFirstName) == true && _utilities.CheckValue(newLastName) == true && _utilities.CheckValue(newDepId) == true && _utilities.CheckIsInt(newDepId) == true && _departamentService.CheckIdIsExist(newDepId) == true;

        }

        private void PrintAllStudents()                                                                     //Atspausdinu visus studentus
        {
            Console.WriteLine("------------------------------------------------------------------");
            _studentService.GetAllStudents().ForEach(student => Console.WriteLine($"ID: {student.id}   Studentas: {student.fName} {student.lName}"));
            Console.WriteLine("------------------------------------------------------------------");
        }


        // -------------------- fakultetas --------------------------------------------------------

        public void UpdateDepartament()                                                                 //Atnaujinti fakulteta
        {
            Console.Clear();
            PrintAllDepartamentsToConsole();
            Console.WriteLine("Pasirinkite fakulteto ID, kuri norite koreguoti:");
            var depId = Console.ReadLine();
            Console.Clear();
            if (depId != string.Empty)                                                                  //tikrinu ar ivesta kokia nors reiksme
            {
                if (CheckDepartamentID(depId) == true)                                                  //tikrinu ar ivestas ID yra tinkamas
                {
                    var departament = _departamentService.GetDepartamentById(depId);
                    Console.WriteLine($"Pasirinkto fakulteto informacija:");
                    Console.WriteLine($"ID: {departament.id},  Pavadinimas: {departament.name}");
                    SelectActionWithDepartament(depId);                                                 //pasirenkame ka norime daryti Trinti ar koreguoti
                }
                else
                {
                    PrintErrorMessageWrongInput();
                    UpdateDepartament();
                }
            }
        }
        private void SelectActionWithDepartament(string depId)                                      //pasirenkame ka norime daryti Trinti ar koreguoti                                          
        {
            PrintOptionsMenu();
            var input = Console.ReadLine();
            if (_utilities.CheckValue(input) == true && _utilities.CheckIsInt(input) == true)      //tikrinu ivesta pasirinkima
            {
                if (input == "1")
                {
                    _departamentService.DeleteDepartamentById(depId);                             //trinti departamenta
                }
                else if (input == "2")
                {
                    ChangeDepartamentInformation(depId);                                          //studento informacijos keitimas pagal pasirinkta ID
                }
            }
            else
            {
                PrintErrorMessageWrongInput();
                UpdateDepartament();                                                              //Grista i meniu vienu lygiu aukstin
            }
        }

        private void ChangeDepartamentInformation(string depId)                                 //fakulteto koregavimas
        {
            Console.Clear();
            PrintDepartamentToConsoleById(depId);                                               //Atspausdinu konkretu fakulteta pagal gauta ID
            Console.WriteLine("Iveskite nauja pavadinima:");
            Departament departament = new Departament();
            departament.name = Console.ReadLine();
            Console.WriteLine("Iveskite PASKAITU ID, kurios turi buti priskirtos fakultetui. PVZ: 1, 2, 3, 4 ir t.t.");
            var stringLectureID = Console.ReadLine();

            
            // tikrinu ar ivestos reiksmes tinkamos
            if (CheckStringIsNotEmpty(departament.name, stringLectureID) == true && _utilities.CheckValue(departament.name) && _utilities.CheckIsInt(stringLectureID) == true)
            {
                _departamentService.UpdateDepartamentById(depId, departament, stringLectureID);
            }
            else
            {
                PrintErrorMessageWrongInput();
                UpdateDepartament();
            }

        }

        private void PrintDepartamentToConsoleById(string depId)                                   //Atspausdinu fakulteto informacija pagal gauta ID
        {
            var dep = _departamentService.GetDepartamentById(depId);
            Console.WriteLine($"ID: {dep.id} | Pavadinimas: {dep.name}");
            Console.WriteLine("Fakultetui priskirtos paskaitos:");
            dep.lectures.ToList().ForEach(e => Console.WriteLine($"ID: {e.id}, pavadinimas: {e.name}"));
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        private void PrintAllDepartamentsToConsole()                                               //Atspausdinu visus fakultetus
        {
            Console.WriteLine("--------------------------------------------------------------------");
            _departamentService.GetAllDepartaments().ForEach(departament => Console.WriteLine($"ID: {departament.id}    Pavadinimas: {departament.name}"));
            Console.WriteLine("--------------------------------------------------------------------");
        }

        private bool CheckDepartamentID(string depId)                                              //tikrinamas fakulteto ID: ar ne kenkejiskas kodas, ar ivestas skaicius, ar egzistuoja
        {
            return _utilities.CheckValue(depId) && _utilities.CheckIsInt(depId) && _departamentService.CheckIdIsExist(depId);
        }

        // ----------------------- paskaitos ----------------------------------------------------
        public void UpdateLecture()
        {
            Console.Clear();
            PrintAllLectures();                                                                    //Atspausdinu visų paskatų lista
            Console.WriteLine("Pasirinkite paskatos ID, kuri norite koreguoti:");
            var lecId = Console.ReadLine();
            Console.Clear();
            if (lecId != string.Empty)
            {
                if (CheckLectureID(lecId) == true)                                                    //Patikrinu ar ivestas tinkamas paskaitos ID
                {
                    var lecture = _lectureService.GetLectureById(Convert.ToInt32(lecId));             //pasiimu paskaitos informacija 
                    Console.WriteLine($"Pasirinktos paskaitos informacija:");
                    Console.WriteLine($"ID: {lecture.id}, pavadinimas: {lecture.name}");
                    Console.WriteLine("Priskirti departamentai:");
                    lecture.departaments.ToList().ForEach(e => Console.WriteLine($"ID: {e.id}, pavadinimas: {e.name}"));
                    SelectActionWithLectureId(lecId);                                                 //kokie veiksmai bus atliekami su pasirinktu studento ID
                }
                else
                {
                    PrintErrorMessageWrongInput();
                    UpdateLecture();
                }
            }

        }

        private void PrintAllLectures()                                                             //Atspausdinu visas paskaitas
        {
            Console.WriteLine("--------------------------------------------------------------------");
            _lectureService.GetAllLecture().ForEach(lec => Console.WriteLine($"ID: {lec.id}    Pavadinimas:{lec.name}"));
            Console.WriteLine("--------------------------------------------------------------------");
        }

        private bool CheckLectureID(string lecId)                                                   //Patikrinu ar ivestas paskaitos ID tinkamas
        {
            return _utilities.CheckValue(lecId) && _utilities.CheckIsInt(lecId) && _lectureService.CheckIdIsExist(lecId);
        }

        private void SelectActionWithLectureId(string lecId)                                        //Trinti ar koreguoti pasirinkta paskaita
        {
            PrintOptionsMenu();
            var input = Console.ReadLine();
            if (_utilities.CheckValue(lecId) == true && _utilities.CheckIsInt(lecId) == true)      //tikrinu ivesta pasirinkima
            {
                if (input == "1")
                {
                    _lectureService.DeleteLectureById(lecId);                                        //trinti departamenta
                }
                else if (input == "2")
                {
                    ChangeLectureInformation(lecId);                                          //paskaitos informacijos keitimas pagal pasirinkta ID
                }
            }
            else
            {
                PrintErrorMessageWrongInput();
                UpdateLecture();                                                              //Grista i meniu vienu lygiu aukstin
            }
        }

        private void ChangeLectureInformation(string lecId)                                      //paskaitos informacijos keitimas pagal pasirinkta ID
        {
            Console.Clear();
            Console.WriteLine("Iveskite nauja paskaitos pavadinima:");
            Lecture lecture = new Lecture();
            lecture.name = Console.ReadLine();
            Console.WriteLine("Iveskite Departamento ID, kuriems turi buti priskirta paskaita. PVZ: 1, 2, 3, 4 ir t.t.");
            var stringDepartamentID = Console.ReadLine();

            if (CheckStringIsNotEmpty(lecture.name, stringDepartamentID) == true && _utilities.CheckValue(lecture.name) == true)
            {
                _lectureService.UpdateDepartamentById(Convert.ToInt32(lecId), lecture, stringDepartamentID);
            }
            else
            {
                PrintErrorMessageWrongInput();
                UpdateLecture();
            }
        }

        // ----------------------- bendros -------------------------------------------------------
        private void PrintErrorMessageWrongInput()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Netinkamas pasirinkimas");
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        private void PrintOptionsMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Trinti");
            Console.WriteLine("2 - Koreguoti");
        }

        private bool CheckStringIsNotEmpty(string inputA, string inputB)
        {
            return (inputA != string.Empty && inputB != string.Empty);
        }
    }
}
