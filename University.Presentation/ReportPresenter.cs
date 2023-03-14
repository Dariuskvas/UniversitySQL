using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using University.BusinessLogic.Interfaces;
using University.Presentation.Interfaces;

namespace University.Presentation
{
    public class ReportPresenter : IReportPresenter
    {
        private readonly IDepartamentService _departamentService;
        private readonly IStudentService _studentService;

        public ReportPresenter(IDepartamentService departamentService, IStudentService studentService)
        {
            _departamentService = departamentService;
            _studentService = studentService;
        }

        public void PrintRaports()
        {
            Console.Clear();
            PrintAllSudentsFromDepartament();
            Console.WriteLine();
            Console.WriteLine(@"/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\");

            Console.WriteLine();
            PrintAllLecturesFromDepartaments();
            Console.WriteLine();
            Console.WriteLine(@"/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\");

            Console.WriteLine();
            PrintAllLecturesByStudents();
            Console.ReadLine();
        }

        private void PrintAllSudentsFromDepartament()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("FAKULTETAI ir jiems priklausantys STUDENTAI.");
            Console.ResetColor();

            foreach (var dep in _departamentService.GetAllDepartaments())
            {
                Console.WriteLine("____________________________________________________________");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Fakulteto pavadinimas: {dep.name}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"Studento ID     Studento vardas     Studento pavarde");
                dep.students.ToList().ForEach(stud => Console.WriteLine($"    {stud.id}               {stud.fName}               {stud.lName}"));
            }
            Console.WriteLine();
        }

        private void PrintAllLecturesFromDepartaments()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FAKULTETAI ir jiems priklausancios PASKAITOS.");
            Console.ResetColor();

            foreach (var dep in _departamentService.GetAllDepartaments())
            {
                Console.WriteLine("____________________________________________________________");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Fakulteto pavadinimas: {dep.name}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"Paskaitos ID     Paskaitos pavadinimas");
                dep.lectures.ToList().ForEach(lec => Console.WriteLine($"    {lec.id}               {lec.name}"));
            }
        }

        private void PrintAllLecturesByStudents()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("STUDENTAI ir jiems priklausancios PASKAITOS.");
            Console.ResetColor();
            foreach (var stud in _studentService.GetAllStudents())
            {
                Console.WriteLine("____________________________________________________________");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Studento: {stud.fName} {stud.lName} paskaitu sarasas");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"Paskaitos ID     Paskaitos pavadinimas");
                stud.departaments.lectures.ToList().ForEach(lec => Console.WriteLine($"    {lec.id}               {lec.name}"));
            }
        }

    }
}
