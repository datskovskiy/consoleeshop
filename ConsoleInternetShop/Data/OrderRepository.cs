using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using ConsoleEShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEShop.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public void CreateOrder(User user, List<OrderItem> orderItems)
        {
            if (user is null)
                return;

            var order = new Order(user.Id, orderItems, "");

            _context.Orders.Add(order);
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(p => p.Id == id);
        }

        public IReadOnlyList<Order> GetUserOrders(User user)
        {
            return _context.Orders.Where(o => o.ClientId == user.Id).ToList();
        }

        public void UpdateStatusOrder(int id, OrderStatus status)
        {
            var order = GetOrderById(id);
            if (order is null)
                return;

            order.Status = status;
        }
    }
}
