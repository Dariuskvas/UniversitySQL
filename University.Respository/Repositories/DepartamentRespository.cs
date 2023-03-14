using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Interfaces;
using University.Respository.Models;

namespace University.Respository.Repositories
{
    public class DepartamentRespository: IDepartamentRespository
    {
        private readonly UniversityDbContext _context;

        public DepartamentRespository(UniversityDbContext context)
        {
            _context = context;
        }

        public void AddDepartament(Departament departament)
        {
            _context.departaments.Add(departament);
        }

        public List<Departament> GetAllDepartametby()
        {
            return _context.departaments
                .Include(x=>x.lectures)
                .Include(x=>x.students)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool CheckIdIsExist(int depID)
        {
            return _context.departaments.Any(x => x.id == depID);
        }

        public Departament GetDepartamentsById(int depId)
        {
            return _context.departaments.Where<Departament>(x => x.id == depId)
                .Include(x=>x.lectures)
                .Include(x=>x.students)
                .FirstOrDefault();
        }

        public void DeleteDepartamentById(int id)
        {
            var departament = _context.departaments.FirstOrDefault(x => x.id == id);
            _context.departaments.RemoveRange(departament);
        }

        public void UpdateDepartamentById(int id, Departament departament)
        {
            var dep = _context.departaments.FirstOrDefault(x => x.id == id);
            if (dep != null)
            {
                dep.name = departament.name;
            }
        }

        public void AddDepartamentLecturesById(int id, Lecture lecture)
        {
            GetDepartamentsById(id).lectures.Add(lecture);
        }

        public void DeleteDepartamentLecturesById(int id, Lecture lecture)
        {
            GetDepartamentsById(id).lectures.Remove(lecture);
        }

        public int CountDep()
        {
            return _context.departaments.Count();
        }

        public int GetLastIdOfDep()
        {
            return _context.departaments.OrderByDescending(d => d.id).Select(d => d.id).FirstOrDefault();
        }
             


    }
}
