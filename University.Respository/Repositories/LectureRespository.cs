using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.Respository.Repositories
{
    public class LectureRespository: ILectureRespository
    {
        private readonly UniversityDbContext _context;
        private readonly IDepartamentRespository _departamentRespository;

        public LectureRespository(UniversityDbContext context, IDepartamentRespository departamentRespository)
        {
            _context = context;
            _departamentRespository = departamentRespository;
        }

        public void AddLecture(Lecture lecture, List<Departament> depList)
        {
            lecture.departaments = depList;
            _context.lectures.Add(lecture);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool CheckIdIsExist(int lecID)
        {
            return _context.lectures.Any(x => x.id == lecID);
        }


        public List<Lecture> GetAllLecture()
        {
            return _context.lectures.ToList();
        }

        public Lecture GetLectureById(int lecId)
        {
            return _context.lectures.Where<Lecture>(x => x.id == lecId)
                .Include(x => x.departaments)
                .FirstOrDefault();
        }

        public void DeleteLectureById(int id)
        {
            var lecture = _context.lectures.FirstOrDefault(s => s.id == id);
            _context.lectures.RemoveRange(lecture);
        }

        public void UpdateLectureById(int id, Lecture lecture)
        {
            var lec = _context.lectures.FirstOrDefault(x => x.id == id);
            if (lec != null)
            {
                lec.name = lecture.name;
            }
        }

        public void AddLectureDepartamentsById(int id, Departament departament)
        {
            GetLectureById(id).departaments.Add(departament);
        }

        public void DeleteLectureDepartamentsById(int id, Departament departament)
        {
            GetLectureById(id).departaments.Remove(departament);
        }

        public int GetLastIdOfLec()
        {
            return _context.lectures.OrderByDescending(d => d.id).Select(d => d.id).FirstOrDefault();
        }
    }
}
