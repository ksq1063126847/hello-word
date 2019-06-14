using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Entity
{
    public class Person
    {
        public Person()
        {
            Company = new HashSet<Company>();
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Company> Company { get; set; }
    }
}
