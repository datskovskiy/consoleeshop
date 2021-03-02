using ConsoleEShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEShop.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        Product GetProductByName(string name);

        IReadOnlyList<Product> GetProducts();

        void CreateProduct(Product entity);

        void UpdateProduct(int id, Product entity);

        void DeleteProduct(int id);
    }
}
