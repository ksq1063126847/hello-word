﻿using Domain.Abstract;
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
            get { return context.Products; }
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry =context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
            }
            context.SaveChanges();
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                    dbEntry.ImageData = product.ImageData;
                }
            }
            context.SaveChanges();
        }
    }
}
