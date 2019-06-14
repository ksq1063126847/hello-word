using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Entity
{
    public class Company
    {
        public Company()
        {
            Persons = new HashSet<Person>();
        }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}
