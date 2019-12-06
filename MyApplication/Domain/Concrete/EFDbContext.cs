using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() { }
        public EFDbContext(string connstr) : base(connstr)
        {

        }
        public DbSet<Product> Products { get; set; }
        
    }
}
