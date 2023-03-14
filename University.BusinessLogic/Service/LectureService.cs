using University.BusinessLogic.Interfaces;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.BusinessLogic.Service
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRespository _lectureRespository;
        private readonly IDepartamentRespository _departamentRespository;
        private readonly IUtilities _utilities;

        public LectureService(ILectureRespository lectureRespository, IUtilities utilities, IDepartamentRespository departamentRespository)
        {
            _lectureRespository = lectureRespository;
            _utilities = utilities;
            _departamentRespository = departamentRespository;
        }

        public void AddLecture(string lectureName, string stringDepartamentId)               //Prideti nauja paskaita ir priskirti departamentams
        {
            Lecture lecture = new Lecture
            {
                name = lectureName
            };
            List<Departament> departaments = new();
            ConvertStringToIntList(stringDepartamentId).ForEach(id => departaments.Add(GetDepartById(id)));

            _lectureRespository.AddLecture(lecture, departaments);
            _lectureRespository.Save();
            PrintSucsessMesage($"Paskaita << {lecture.name} >> sukurta sekmingai!");
        }

        public bool CheckIdIsExist(string lecID)                                            //Tikrinu ar toks paskaitos ID egzistuoja
        {
            return _lectureRespository.CheckIdIsExist(Convert.ToInt32(lecID));
        }

        public List<Lecture> GetAllLecture()                                                //Grazina visu laskaitu sarasa
        {
            return _lectureRespository.GetAllLecture();
        }

        public Lecture GetLectureById(int lecId)                                            //Paskaitu informacija pagal ID
        {
            return _lectureRespository.GetLectureById(Convert.ToInt32(lecId));
        }
        public void DeleteLectureById(string id)                                            //Paskaitos istrinimas
        {
            _lectureRespository.DeleteLectureById(Convert.ToInt32(id));
            _lectureRespository.Save();
            PrintSucsessMesage($"Paskaita ID: << {id} >> istrinta sekmingai!");
        }

        public List<int> CheckLectureIDListIsExist(List<int> inputList)                     //Tikrinu ar pateikti paskaitu ID egzistuoja, neegzistuojancius istrinu
        {
            inputList.RemoveAll(x => !CheckIdIsExist(x.ToString()));
            return inputList;
        }

        public void UpdateDepartamentById(int lecId, Lecture lecture, string newlistDepId)      //ATnaujinu paskaitu informacija ir kuriems departamentams priskirti
        {
            List<int> oldDepartamensId = GetOldDepartamentsIdByLecture(lecId);
            List<int> newDepartamensId = ConvertStringToIntList(newlistDepId);
            CompareOldAndNewIdLists(lecId, oldDepartamensId, newDepartamensId);
            _lectureRespository.UpdateLectureById(lecId, lecture);
            _lectureRespository.Save();
            PrintSucsessMesage($"Paskaita ID: << {lecId} >> atnaujinta sekmingai!");
        }

        private List<int> GetOldDepartamentsIdByLecture(int id)                                 //Issitraukiu departamentu ID, kuriems priskirta paskaita
        {
            List<int> oldDepartamentId = new List<int>();
            foreach (var dep in _lectureRespository.GetLectureById(id).departaments)
            {
                oldDepartamentId.Add(dep.id);
            }
            return oldDepartamentId;
        }

        private List<int> ConvertStringToIntList(string newlistDepId)
        {
            List<int> newLectureID = _utilities.ConverteStringToIntList(newlistDepId);       //string konvertuoju i List<int>
            newLectureID.RemoveAll(x => !_departamentRespository.CheckIdIsExist(x));         //patikrinu ar ID egzistuoja, jei ne istrinu is List<int>
            return newLectureID;
        }

        private void CompareOldAndNewIdLists(int lecId, List<int> oldDepartamensId, List<int> newDepartamensId)    //Palyginu esamus departamento ID lista su nauju kuriems priskirta konkreti paskaita.
        {
            foreach (int depId in oldDepartamensId)
            {
                if (!newDepartamensId.Contains(depId))        //Jei paskaita toks fakulteto ID naujai nepriskirtas, jis istrinamas             
                {
                    _lectureRespository.DeleteLectureDepartamentsById(lecId, GetDepartById(depId));
                }
            }

            foreach (int newDepId in newDepartamensId)       //Jei paskaitai toks fakulteto ID naujai priskirtas(nebuvo sename LIST), jis pridedamas
            {
                if (!oldDepartamensId.Contains(newDepId))
                {
                    _lectureRespository.AddLectureDepartamentsById(lecId, GetDepartById(newDepId));
                }
            }
        }

        private void PrintSucsessMesage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(2000);
        }

        private Departament GetDepartById(int id)
        {
            return _departamentRespository.GetDepartamentsById(id);
        }
        public int GetLastIdOfLec()
        {
            return _lectureRespository.GetLastIdOfLec();
        }

    }
}
