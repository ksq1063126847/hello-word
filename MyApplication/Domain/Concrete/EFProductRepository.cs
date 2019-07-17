using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get{ return context.Products; }
        }
    }
}
