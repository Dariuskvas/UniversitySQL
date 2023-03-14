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
    public class StudentRespository: IStudentRespository
    {
        private readonly UniversityDbContext _context;

        public StudentRespository(UniversityDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.students.Add(student);
        }

        public Student GetStudentById(int id)
        {
            return _context.students.Where<Student>(x =>x.id == id)
                .Include(x=>x.departaments)
                .Include(x=>x.departaments.lectures)
                .FirstOrDefault();
        }

        public List<Student> GetAllStudents()
        {
            return _context.students
                .Include(x => x.departaments)
                .Include(x => x.departaments.lectures)
                .ToList();
        }

        public bool CheckStudentIdIsExist(int studID)
        {
            return _context.students.Any(x => x.id == studID);
        }

        public void UpdateStudentById(int id, Student student)
        {
            var stud = _context.students.FirstOrDefault(s => s.id == id);
            if (stud != null)
            {
                stud.fName = student.fName;
                stud.lName= student.lName;
                stud.departaments= student.departaments;
            }
        }

        public void DeleteStudentById(int id)
        {
            var student = _context.students.FirstOrDefault(s => s.id == id);
            _context.students.RemoveRange(student);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
