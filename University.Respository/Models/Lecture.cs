using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Respository.Models
{
    public class Lecture
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Departament> departaments { get; set; }
    }
}
