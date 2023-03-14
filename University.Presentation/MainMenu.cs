using Newtonsoft.Json.Linq;
using University.BusinessLogic.Interfaces;
using University.Presentation.Interfaces;

namespace University.Presentation
{
    public class MainMenu : IMainMenu
    {
        private readonly IInputForNewSubjects _inputForNewSubjects;
        private readonly IInputForSubjectCorection _inputForSubjectCorection;
        private readonly IReportPresenter _reportPresenter;
        private readonly IDepartamentService _departamentService;
        private readonly IDbService _dbService;


        public MainMenu(IInputForNewSubjects inputForNewSubjects, IInputForSubjectCorection inputForSubjectCorection, IReportPresenter reportPresenter, IDepartamentService departamentService, IDbService dbService)
        {
            _inputForNewSubjects = inputForNewSubjects;
            _inputForSubjectCorection = inputForSubjectCorection;
            _reportPresenter = reportPresenter;
            _departamentService = departamentService;
            _dbService = dbService;
        }

        //tikrinu ar DB tuscia, jei tuscia sukeliama pradine info is failo. jei yra duomenu eina i Meniu.
        public void CheckIsDbEmptyBeforStart()
        {
            if (_departamentService.CheckDbIsEmpty() == false)
            {
                GetMenuMaine();                                                                                     //Pagrindinis Meniu
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Duomenu baze tuscia. Importuojami duomenys is JSON filo. Truputi palukite.");
                _dbService.ImportDataFromFile(JToken.Parse(File.ReadAllText("../../../firstData.json")));          //Ikeliu pradnius duomenis is failo
                GetMenuMaine();
                Environment.Exit(0);
            }
        }


        public void GetMenuMaine()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintMenuMaineText();

                switch (Console.ReadLine())
                {
                    case "1":
                        _reportPresenter.PrintRaports();                             //Raportai        
                        break;
                    case "2":
                        GetMenuForDatabase();                                        //Kurti/koreguoti duomenis is DB 
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Blogai ivetas pasirinkimas");
                        break;
                }
            }
        }

        private void GetMenuForDatabase()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                PrintMenuForDatabaseText();

                switch (Console.ReadLine())
                {
                    case "1":
                        _inputForNewSubjects.AddDepartamet();               //sukurti nauja fakulteta                  
                        break;
                    case "2":
                        _inputForNewSubjects.AddLecture();                  //sukurti nauja paskaita
                        break;
                    case "3":
                        _inputForNewSubjects.AddStudent();                  //sukurti nauja studenta ir priskirti departamentui
                        break;
                    case "4":
                        _inputForSubjectCorection.UpdateDepartament();      //koreguoti fakulteto informacija ir padaryti paskaitu priskirima
                        break;
                    case "5":
                        _inputForSubjectCorection.UpdateStudent();          //koreguoti studento duomenis ir kuriam departamentui priskirtas
                        break;
                    case "6":
                        _inputForSubjectCorection.UpdateLecture();          //koreguoti paskaitu duomenis ir kuriam departamentui priskirtas
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Blogai ivetas pasirinkimas");
                        break;
                }

            }

        }

        private static void PrintMenuMaineText()
        {
            Console.WriteLine("Pasirinkite iš norimo Menu:");
            Console.WriteLine(" 1 - Ataskaitos");
            Console.WriteLine(" 2 - Darbas su duomenų baze");
            Console.WriteLine(" 3 - Exit");
        }

        private static void PrintMenuForDatabaseText()
        {
            Console.WriteLine("Pasirinkite iš norimo Menu:");
            Console.WriteLine(" 1 - Sukurti fakulteta");
            Console.WriteLine(" 2 - Sukurti paskaitas");
            Console.WriteLine(" 3 - Sukurti studenta");
            Console.WriteLine(" 4 - Koreguoti fakulteta");
            Console.WriteLine(" 5 - Koreguoti studenta");
            Console.WriteLine(" 6 - Koreguoti paskaitas");
            Console.WriteLine(" 7 - Exit");
        }


    }
}
