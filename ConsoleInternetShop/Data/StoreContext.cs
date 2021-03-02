using ConsoleEShop.Entities;
using System.Collections.Generic;

namespace ConsoleEShop.Data
{
    public class StoreContext
    {
        public StoreContext()
        {
            Users = new List<User>();
            Products = new List<Product>();
            Orders = new List<Order>();

            Seed();
        }

        public List<Product> Products { get; set; }
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }

        private void Seed()
        {
            Products.Add(new Product {Id = 1, Name =  "Orange", Price = 100, Category = "Fruits"});
            Products.Add(new Product {Id = 2, Name = "Melon", Price = 150, Category = "Fruits"});
        }
    }
}
