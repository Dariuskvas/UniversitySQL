using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Respository.Models;

namespace University.Respository
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Departament> departaments { get; set; }
        public DbSet<Lecture> lectures { get; set; }
        public DbSet<Student> students { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {

        }
    }
}
