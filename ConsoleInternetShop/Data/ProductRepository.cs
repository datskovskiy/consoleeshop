using ConsoleEShop.Interfaces;
using ConsoleEShop.Entities;
using System.Collections.Generic;

namespace ConsoleEShop.Data
{
    class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public void CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product is null)
                return;

            _context.Products.Remove(product);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(p => p.Id == id);
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.Find(p => p.Name == name);
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return _context.Products;
        }

        public void UpdateProduct(int id, Product entity)
        {
            var product = GetProductById(id);
            if (product is null)
                return;

            product.Name = entity.Name;
            product.Category = entity.Category;
            product.Description = entity.Description;
            product.Price = entity.Price;
        }
    }
}
