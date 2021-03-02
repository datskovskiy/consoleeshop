using ConsoleEShop.Interfaces;

namespace ConsoleEShop.Entities
{
    public class Product : BaseEntity, IProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}