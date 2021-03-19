using ConsoleEShop.Entities;
using ConsoleEShop.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Interfaces
{
    public interface IOrderService
    {
        Order GetOrderById(int id);

        IReadOnlyList<Order> GetUserOrders(IUser user);

        void CreateOrder(Order order);

        void UpdateStatusOrder(int id, OrderStatus status);
    }
}
