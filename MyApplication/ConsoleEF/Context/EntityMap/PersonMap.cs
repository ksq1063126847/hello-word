using ConsoleEF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Context.EntityMap
{
     public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasMany<Company>(p => p.Company).WithMany(p => p.Persons)
                .Map(p => { p.MapLeftKey("PersonID"); p.MapRightKey("CompanyId"); p.ToTable("PersonCompanyMap"); });
        }
    }
}
