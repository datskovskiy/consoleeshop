using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int clientId, string address);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(int clientId);

        Task<Order> GetOrderByIdAsync(int id, int clientId);
    }
}
