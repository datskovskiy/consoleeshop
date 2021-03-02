using Core.Entities;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class StoreContext
    {
        public StoreContext()
        {
        }

        public IList<Product> Products { get; set; }
        public IList<User> Users { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
