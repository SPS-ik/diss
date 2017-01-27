using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FmsModel.Manufacturing
{
    public class Warehouse: ILocation
    {
        public List<Product> Products { get; set; }

        public Product Get(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
            }
            return product;
        }

        public void Put(Product product)
        {
            Products.Add(product);
        }

        public void Put(List<Product> products)
        {
            Products.AddRange(products);
        }
    }
}