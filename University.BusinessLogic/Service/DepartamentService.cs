using University.BusinessLogic.Interfaces;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.BusinessLogic.Service
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IDepartamentRespository _departamentRespository;
        private readonly ILectureRespository _lectureRespository;
        private readonly ILectureService _lectureService;
        private readonly IUtilities _utilities;

        public DepartamentService(IDepartamentRespository departamentRespository, IUtilities utilities, ILectureService lectureService, ILectureRespository lectureRespository)
        {
            _departamentRespository = departamentRespository;
            _utilities = utilities;
            _lectureRespository = lectureRespository;
            _lectureService = lectureService;
        }

        public void AddDepartament(string depName)                       //Prideti nauja fakulteta
        {
            Departament departament = new Departament();
            departament.name = depName;
            _departamentRespository.AddDepartament(departament);
            _departamentRespository.Save();
            PrintSucsessMesage($"Fakultetas << {departament.name} >> sukurtas sekmingai!");
        }

        public List<Departament> GetAllDepartaments()                    //Grazinami visi fakultetai
        {
            return _departamentRespository.GetAllDepartametby();
        }

        public bool CheckIdIsExist(string depId)                        //tikrinu ar pateiktas ID egzistuoja
        {
            return _departamentRespository.CheckIdIsExist(Convert.ToInt32(depId));
        }

        public Departament GetDepartamentById(string depId)             //Gaunu fakulteto informacija pagal pateikta ID
        {
            return _departamentRespository.GetDepartamentsById(Convert.ToInt32(depId));
        }

        public void DeleteDepartamentById(string depId)                 //Istrinu fakulteta
        {
            _departamentRespository.DeleteDepartamentById(Convert.ToInt32(depId));
            _departamentRespository.Save();
            PrintSucsessMesage($"Fakultetas ID: << {depId} >> istrintas sekmingai!");
        }

        public void UpdateDepartamentById(string id, Departament departament, string stringLectureID)
        {
            List<int> oldLectureID = GetOldLectureIdByDepartament(id);                              //esami LectureID priskirti Departament
            List<int> newLectureID = ConvertStringToLectureIdList(stringLectureID);                 // nauji LectureID, kuriuos reikia priskirti Departament 
            CompareOldAndNewIdLists(id, oldLectureID, newLectureID);

            _departamentRespository.UpdateDepartamentById(Convert.ToInt32(id), departament);        //Uzsetinti Departamenta su jaujokmis reiksmemis
            _departamentRespository.Save();
            PrintSucsessMesage($"Fakultetas ID: << {id} >> atnaujintas sekmingai!");
        }

        private List<int> ConvertStringToLectureIdList(string stringLectureID)                  // is gauto string "isrenku" lecture ID
        {
            List<int> newLectureID = _utilities.ConverteStringToIntList(stringLectureID);       //string konvertuoju i List<int>
            return _lectureService.CheckLectureIDListIsExist(newLectureID);                     //Grazinu nauja Lista ir patikrinu ar ID egzistuoja, jei ne istrinu is List<int>
        }

        //tikrinamas koreguojamo fakulteto paskautu sarasas. Jei paskaita egzistuoja ji paliekama, jei paskaitos naujame sarase nera ji trinama, jei ji yra o sename nera ji pridedama
        private void CompareOldAndNewIdLists(string id, List<int> oldLectureID, List<int> newLectureID)
        {
            foreach (int lecId in oldLectureID)
            {
                if (!newLectureID.Contains(lecId))
                {
                    _departamentRespository.DeleteDepartamentLecturesById(Convert.ToInt32(id), _lectureRespository.GetLectureById(lecId));      //istrinu lectureID kuriu nebereikia priskirti Departament
                }
            }

            foreach (int newLecId in newLectureID)
            {
                if (!oldLectureID.Contains(newLecId))
                {
                    _departamentRespository.AddDepartamentLecturesById(Convert.ToInt32(id), _lectureRespository.GetLectureById(newLecId));        //prideti nauja lectureID pasirinktam Departament
                }
            }
        }

        private List<int> GetOldLectureIdByDepartament(string id)                                       //gaunu sena LectureID sarasa kuris priskirtas Departament
        {
            List<int> oldLectureID = new List<int>();
            foreach (var lecture in _departamentRespository.GetDepartamentsById(Convert.ToInt32(id)).lectures)
            {
                oldLectureID.Add(lecture.id);
            }
            return oldLectureID;
        }

        public List<int> CheckDepartamentIdListIsExist(List<int> inputList)     //Tikrinu ar pateiktame ID liste yra egzistuojantys fakulteto ID. Jei ju nera is listo istrinami
        {
            inputList.RemoveAll(x => !CheckIdIsExist(x.ToString()));
            return inputList;
        }

        private void PrintSucsessMesage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        public bool CheckDbIsEmpty()                                //Tikrinu ar DB fakultetu tra tuscia
        {
            if (_departamentRespository.CountDep() == 0)
            {
                return true;
            }
            return false;
        }

        public int GetLastIdOfDep()                                 //issitraukiu paskutini fakulteto ID
        {
            return _departamentRespository.GetLastIdOfDep();
        }


    }

}