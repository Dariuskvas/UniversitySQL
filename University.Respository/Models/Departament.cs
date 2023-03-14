using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Respository.Models
{
    public class Departament
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Lecture> lectures { get; set; }
        public ICollection<Student> students { get; set; }
    }
}
