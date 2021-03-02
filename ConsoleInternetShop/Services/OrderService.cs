using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleEShop.Entities;

namespace ConsoleEShop.Services
{
    class OrderService //: IOrderService
    {
        public Task<Order> CreateOrderAsync(int clientId, string address)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
