namespace ConsoleEShop.Entities
{
    public class OrderItem
    {
        public int ProductItemId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(int productItemId, decimal price, int quantity)
        {
            ProductItemId = productItemId;
            Price = price;
            Quantity = quantity;
        }
    }
}
