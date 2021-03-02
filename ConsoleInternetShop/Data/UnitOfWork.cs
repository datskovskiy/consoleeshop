using ConsoleEShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEShop.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => new ProductRepository(_context);

        public IAuthRepository AuthRepository => new AuthRepository(_context);

    }
}
