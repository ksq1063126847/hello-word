using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            var line = lineCollection.FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, quantity = quantity });
            }
            else
                line.quantity += quantity;
        }
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines()
        {
            return lineCollection;
        }

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int quantity { get; set; }
    }
}
