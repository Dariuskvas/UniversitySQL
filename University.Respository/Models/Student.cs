using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Respository.Models
{
    public class Student
    {
        public int id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }

        public Departament departaments { get; set;}
    }
}
