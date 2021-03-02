using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Order: BaseEntity
    {
        public Order(string address, decimal subtotal, int clientId, IReadOnlyList<OrderItem> orderItems)
        {
            Address = address;
            Subtotal = subtotal;
            ClientId = clientId;
            OrderItems = orderItems;
        }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public decimal Subtotal { get; set; }
        public int ClientId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;

        public IReadOnlyList<OrderItem> OrderItems { get; set; }
    }
}
