using ConsoleEF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEF.Context.EntityMap
{
    public class DonatorMap : EntityTypeConfiguration<Donator>
    {
        public DonatorMap()
        {
            ToTable("Donator");
            //Property(p => p.Name).IsRequired().HasMaxLength(100);
            HasMany(p => p.PayWays).WithOptional().HasForeignKey(p => p.DonatorID).WillCascadeOnDelete(false);    
        }
    }
}
