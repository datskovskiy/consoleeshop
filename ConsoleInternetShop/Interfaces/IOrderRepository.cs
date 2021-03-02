
using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(int id);

        IReadOnlyList<Order> GetUserOrders(User user);

        void CreateOrder(User user, List<OrderItem> orderItems);

        void UpdateStatusOrder(int id, OrderStatus status);
    }
}
